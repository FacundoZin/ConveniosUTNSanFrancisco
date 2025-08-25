using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Helpers.Validators;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Linq.Expressions;

namespace APIconvenios.Services
{
    public class ConvenioEspecificoService : IConvenioEspecifcoService
    {
        private readonly _UnitOfWork _UnitOfWork;
        private readonly ILogger<ConvenioEspecificoService> _Logger;
        public ConvenioEspecificoService(_UnitOfWork unitOfWork, ILogger<ConvenioEspecificoService> logger, ConvenioQueryObjectValidator queryvalidator)
        {
            _UnitOfWork = unitOfWork;
            _Logger = logger;
        }

        public async Task<Result<ConvenioCreated>> CreateConvenioEspecifico(InsertConvenioEspecificoDto DtoConvenio,
            List<InsertInvolucradosDto> DtoInvolucrados)
        {
            try
            {
                if(await _UnitOfWork._ConvEspReadRepository .TitleExist(DtoConvenio.Titulo))
                    return Result<ConvenioCreated>.Error("el titulo de convenio ingresado ya existe", 409);


                var Involucrados = DtoInvolucrados.ToInvolucrados();

                foreach (var inv in Involucrados)
                {
                    if (inv.Id != 0)
                    {
                        _UnitOfWork.MarkAsExisting(inv);
                    }
                }

                var convenio = DtoConvenio.ToConvenioEspecifico(Involucrados);

                _UnitOfWork._ConvenioEspecificoRepository.CreateConvenio(convenio);

                await _UnitOfWork.Save();

                var result = new ConvenioCreated
                {
                    Id = convenio.Id,
                    ConvenioType = "especifico",
                };

                return Result<ConvenioCreated>.Exito(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error al crear el convenio especifico: {ex.Message} ");
                return Result<ConvenioCreated>.Error($"Error al crear el convenio especifico", 500);
            }
        }

        public async Task<Result<object?>> DeleteConvenioEspecifico(int id)
        {
            try
            {
                var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(id);

                if (convenio == null)
                    return Result<object?>.Error($"el convenio no existe", 404);

                if (!await _UnitOfWork._ConvenioEspecificoRepository.Delete(convenio))
                {
                    _Logger.LogError($"Error al eliminar el convenio especifico con id {id}");
                    return Result<object?>.Error($"Error al eliminar el convenio especifico", 500);
                }

                return Result<object?>.Exito(null);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error al eliminar el convenio especifico: {ex.Message} ");
                return Result<object?>.Error($"Error al eliminar el convenio especifico", 500);
            }
        }

        public async Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoDto Dto)
        {
            var ConvenioOriginal = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(Dto.Id);

            if (ConvenioOriginal == null)
                return Result<object?>.Error($"el convenio no existe", 404);

            if (await _UnitOfWork._ConvEspReadRepository.TitleExistForUpdate(ConvenioOriginal.Titulo, ConvenioOriginal.Id))
                return Result<object?>.Error("error: ya hay un convenio especifico con el titulo que ha ingresado", 409);

            bool exit = await _UnitOfWork._ConvenioEspecificoRepository.ModificarConvenioEspecifico(ConvenioOriginal.UpdateConvenio(Dto));

            if (!exit)
            {
                _Logger.LogError($"Error al editar el convenio especifico con id {Dto.Id}");
                return Result<object?>.Error($"Error al editar el convenio especifico", 500);
            }

            return Result<Object?>.Exito(null);
        }

        public async Task<Result<InfoConvenioEspeficoDto>> ObtenerConvenioEspecificoCompleto(int id)
        {
            var convenio = await _UnitOfWork._ConvEspReadRepository.GetConvenioEspecificoCompleto(id);

            if(convenio == null) return Result<InfoConvenioEspeficoDto>.Error("El convenio especifico no existe", 404);
            

            return Result<InfoConvenioEspeficoDto>.Exito(convenio);
        }
    }
}
