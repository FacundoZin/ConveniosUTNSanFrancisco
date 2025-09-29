using APIconvenios.Commands.ConvenioEspecificoCommands;
using APIconvenios.Commands.ConvenioEspecificoCommands.Commands;
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
        public ConvenioEspecificoService(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<ConvenioCreated>> CreateConvenioEspecifico(CargarConvenioEspecificoRequestDto Dto)
        {
            var Convenio = Dto.InsertConvenioDto.UploadData();

            List<IConvEspCommand> Commnands = new List<IConvEspCommand>();

            if (Dto.InsertInvolucradosDto != null)
                Commnands.Add(new InsertInvolucradorToConvEspCmd(Dto.InsertInvolucradosDto));

            if (Dto.idCarreras != null)
                Commnands.Add(new LinkCarrerasCmd(Dto.idCarreras));

            if (Dto.empresaDto != null)
                Commnands.Add(new LinkEmpresaToEspecificoCmd(Dto.empresaDto));

            if (Dto.IdConvenioMarcoVinculado != null)
                Commnands.Add(new LinkerConvMarcoCmd((int)Dto.IdConvenioMarcoVinculado));

            foreach(var command in Commnands)
                await command.ExecuteAsync(Convenio, _UnitOfWork);

            int rowsAffected = await _UnitOfWork.Save();

            if (rowsAffected > 0)
                return Result<ConvenioCreated>.Exito(new ConvenioCreated { Id = Convenio.Id, ConvenioType = "especifico" });
            else
                return Result<ConvenioCreated>.Error("No se pudo crear el convenio especifico.", 500);
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
                    return Result<object?>.Error($"Error al eliminar el convenio especifico", 500);
                }

                return Result<object?>.Exito(null);
            }
            catch (Exception ex)
            {
                return Result<object?>.Error($"Error al eliminar el convenio especifico", 500);
            }
        }

        public async Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoRequestDto Dto)
        {
            var Convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(Dto.UpdateConvenioDto.Id);

            if(Convenio == null) return Result<object?>.Error("El convenio que quiere actualizar no existe", 404);

            var commands = new List<IConvEspCommand>();

            Convenio.UpdateConvenio(Dto.UpdateConvenioDto);

            if(Dto.InsertInvolucradosDtos != null && Dto.InsertInvolucradosDtos.Any())
                commands.Add(new InsertInvolucradorToConvEspCmd(Dto.InsertInvolucradosDtos));
            if(Dto.idCarreras != null && Dto.idCarreras.Length > 0)
                commands.Add(new LinkCarrerasCmd(Dto.idCarreras));
            if(Dto.empresaDto != null)
                commands.Add(new LinkEmpresaToEspecificoCmd(Dto.empresaDto));
            if(Dto.IdsInvolucraodsEliminados != null && Dto.IdsInvolucraodsEliminados.Any())
                commands.Add(new UnlinkInvolucradosCmd(Dto.IdsInvolucraodsEliminados));
            if (Dto.IdConvenioMarcoVinculado != null)
                commands.Add(new LinkerConvMarcoCmd((int)Dto.IdConvenioMarcoVinculado));
            if (Dto.DesvincularConvenioMarco)
                commands.Add(new UnlinkConvMarcoCmd());
            if(Dto.DesvincularEmpresa)
                commands.Add(new UnlinkEmpresaFromEspecificoCmd());

            foreach (var command in commands)
                await command.ExecuteAsync(Convenio, _UnitOfWork);

            int rowsAffected = await _UnitOfWork.Save();


            if (rowsAffected > 0)
                return Result<object?>.Exito(new ConvenioCreated { Id = Convenio.Id, ConvenioType = "especifico" });
            else
                return Result<object?>.Error("No se pudo actualizar el convenio especifico.", 500);
        }

        public async Task<Result<InfoConvenioEspeficoDto>> ObtenerConvenioEspecificoCompleto(int id)
        {
            var convenio = await _UnitOfWork._ConvEspReadRepository.GetConvenioEspecificoCompleto(id);

            if(convenio == null) return Result<InfoConvenioEspeficoDto>.Error("El convenio especifico no existe", 404);
            

            return Result<InfoConvenioEspeficoDto>.Exito(convenio);
        }
    }
}
