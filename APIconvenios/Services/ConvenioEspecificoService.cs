using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;
using System;

namespace APIconvenios.Services
{
    public class ConvenioEspecificoService : IConvenioEspecifcoService
    {
        private readonly _UnitOfWork _UnitOfWork;
        private readonly ILogger _Logger;
        public ConvenioEspecificoService(_UnitOfWork unitOfWork, ILogger logger)
        {
            _UnitOfWork = unitOfWork;
            _Logger = logger;
        }   

        public async Task<Result<object?>> CreateConvenioEspecifico(InsertConvenioEspecificoDto DtoConvenio, 
            List<InsertInvolucradosDto> DtoInvolucrados)
        {
            try
            {
                var Involucrados = DtoInvolucrados.ToInvolucrados();

                foreach(var inv in Involucrados)
                {
                    if(inv.Id != 0)
                    {
                        _UnitOfWork.MarkAsExisting(inv);
                    }
                }

                _UnitOfWork._ConvenioEspecificoRepository.CreateConvenio( new ConvenioEspecifico
                {
                    
                    numeroconvenio= DtoConvenio.numeroconvenio,
                    FechaFirmaConvenio = DtoConvenio.FechaFirmaConvenio,
                    FechaInicioActividades = DtoConvenio.FechaInicioActividades,
                    FechaFinConvenio = DtoConvenio.FechaFinConvenio,
                    ComentarioOpcional = DtoConvenio.ComentarioOpcional,
                    Involucrados = Involucrados
                });

                await _UnitOfWork.Save();

                return Result<object?>.Exito(null);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error al crear el convenio especifico: {ex.Message} ");
                return Result<object?>.Error($"Error al crear el convenio especifico", 500);
            }
        }

        public async Task<Result<object?>> DeleteConvenioEspecifico(int id)
        {
            try
            {
                var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(id);
                
                if(convenio == null)
                    return Result<object?>.Error($"el convenio no existe", 404);

                if(! await _UnitOfWork._ConvenioEspecificoRepository.Delete(convenio))
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

            bool exit = await _UnitOfWork._ConvenioEspecificoRepository.ModificarConvenioEspecifico(ConvenioOriginal.UpdateConvenio(Dto));

            if (!exit)
            {
                _Logger.LogError($"Error al editar el convenio especifico con id {Dto.Id}");
                return Result<object?>.Error($"Error al editar el convenio especifico", 500);
            }

            return Result<Object?>.Exito(null); 
        }

        public Task<Result<List<ConvenioEspecificoDto>>> ListarConveniosEspecificos(ConvenioQueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<Result<InfoConvenioMarcoDto>> ObtenerConvenioEspecificoCompleto(int id)
        {
            throw new NotImplementedException();
        }
    }
}
