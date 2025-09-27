using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using APIconvenios.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Helpers.Validators;
using APIconvenios.UnitOfWork;
using APIconvenios.DTOs.Empresa;
using Microsoft.Data.Sqlite;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Commands.ConvenioMarco.commands;
using APIconvenios.Commands.ConvenioMarcoCommands.commands;
using APIconvenios.Commands.ConvenioMarcoCommands;

namespace APIconvenios.Services
{
    public class ConveniosMarcosServices : IConvenioMarcoService
    {
        private readonly _UnitOfWork _UnitOfWork;

        public ConveniosMarcosServices(_UnitOfWork unitOfWork,ILogger<ConveniosMarcosServices> logger, ConvenioQueryObjectValidator queryValidator)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> ActualizarConvenioMarco(UpdateConvenioMarcoRequetsDto requetsDto)
        {
            var Convenio = await _UnitOfWork._ConvenioMarcoRepository.GetByid(requetsDto.UpdateConvenioMarcoDto.Id);
            if (Convenio == null) return Result<bool>.Error("El convenio que quiere actualizar no existe", 404);

            var commands = new List<IConvMarcoCommand>();

            if (requetsDto.UpdateConvenioMarcoDto != null)
                commands.Add(new UpdateConvMarcoDataCmd(requetsDto.UpdateConvenioMarcoDto));

            if (requetsDto.InsertEmpresaDto != null && requetsDto.InsertEmpresaDto.Id != null)
                commands.Add(new LinkEmpresaToMarcoCmd(requetsDto.InsertEmpresaDto));

            if (requetsDto.EmpresaDesvinculada)
                commands.Add(new UnlinkEmpresaFromMarcoCmd());

            if (requetsDto.IdsConveniosEspecificosParaVincular?.Any() == true)
                commands.Add(new LinkerConvEspCmd(requetsDto.IdsConveniosEspecificosParaVincular));

            if (requetsDto.IdsConveniosEspecificosParaDesvincular?.Any() == true)
                commands.Add(new UnlinkConvEspCmd(requetsDto.IdsConveniosEspecificosParaDesvincular));

            foreach (var cmd in commands)
                await cmd.ExecuteAsync(Convenio, _UnitOfWork);

            int rowsAffected = await _UnitOfWork.Save();

            if (rowsAffected > 0)
                return Result<bool>.Exito(true);
            else
                return Result<bool>.Error("No se pudo actualizar el convenio marco.", 500);
        }

        public async Task<Result<bool>> BorrarConvenioMarco(int id)
        {
            try
            {
                var convenio = await _UnitOfWork._ConvenioMarcoRepository.GetByid(id);

                if (convenio == null) return Result<bool>.Error("El convenio que quiere borrar no existe", 404);

                bool exit = await _UnitOfWork._ConvenioMarcoRepository.Delete(convenio);

                if (!exit)
                {
                    return Result<bool>.Error("Ocurrio un error al eliminar el convenio marco", 500);
                }

                return Result<bool>.Exito(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error("Ocurrio un error al eliminar el convenio marco", 500);
            }
        }

        public async Task<Result<InfoConvenioMarcoDto?>> ObtenerConvenioMarcoCompleto(int id)
        {
            try
            {
                var InfoConvenio = await _UnitOfWork._ConvenioMarcoReadRepository.GetConvenioMarcosCompleto(id);

                if (InfoConvenio == null)
                {
                    return Result<InfoConvenioMarcoDto?>.Error("No se encontró el convenio marco solicitado", 404);
                }

                return Result<InfoConvenioMarcoDto?>.Exito(InfoConvenio);
            }
            catch (SqliteException ex)
            {
                return Result<InfoConvenioMarcoDto?>.Error("error en la DB", 503);
            }
            catch (InvalidOperationException ex)
            {
                return Result<InfoConvenioMarcoDto?>.Error("Estado del sistema no válido", 500);
            }
            catch (Exception ex)
            {
                return Result<InfoConvenioMarcoDto?>.Error("Error inesperado", 500);
            }
        }

        public async Task<Result<ConvenioCreated>> CargarConvenioMarco(CargarConvenioMarcoRequestDto requestDto)
        {
            var convenio = new ConvenioMarco();

            convenio.UploadData(requestDto.InsertConvenioDto);

            var commands = new List<IConvMarcoCommand>();

            if (requestDto.InsertEmpresaDto != null && requestDto.InsertEmpresaDto.Id != null)
                commands.Add(new LinkEmpresaToMarcoCmd(requestDto.InsertEmpresaDto));

            if (requestDto.IdsConveniosEspecificosParaVincular?.Any() == true)
                commands.Add(new LinkerConvEspCmd(requestDto.IdsConveniosEspecificosParaVincular));


            foreach (var cmd in commands)
                await cmd.ExecuteAsync(convenio, _UnitOfWork);

            int rowsAffected = await _UnitOfWork.Save();

            if (rowsAffected > 0)
                return Result<ConvenioCreated>.Exito(new ConvenioCreated { Id = convenio.Id, ConvenioType = "marco" });
            else
                return Result<ConvenioCreated>.Error("No se pudo crear el convenio marco.", 500);

        }
    }
}