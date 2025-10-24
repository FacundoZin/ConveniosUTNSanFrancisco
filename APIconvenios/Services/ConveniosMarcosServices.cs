﻿using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using APIconvenios.Common;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
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

        public ConveniosMarcosServices(_UnitOfWork unitOfWork, ConveniosFilterService FilterService)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> ActualizarConvenioMarco(UpdateConvenioMarcoRequetsDto requetsDto)
        {
            var Convenio = await _UnitOfWork._ConvenioMarcoReadRepository.GetByidWithConvEspecifico(requetsDto.UpdateConvenioMarcoDto.Id);
            if (Convenio == null) return Result<bool>.Error("El convenio que quiere actualizar no existe", 404);

            var commands = new List<IConvMarcoCommand>();

            if (requetsDto.UpdateConvenioMarcoDto != null)
                commands.Add(new UpdateConvMarcoDataCmd(requetsDto.UpdateConvenioMarcoDto));

            if (requetsDto.InsertEmpresaDto != null)
                commands.Add(new LinkEmpresaToMarcoCmd(requetsDto.InsertEmpresaDto));

            if (requetsDto.EmpresaDesvinculada)
                commands.Add(new UnlinkEmpresaFromMarcoCmd());

            if (requetsDto.IdsConveniosEspecificosParaVincular != null
                && requetsDto.IdsConveniosEspecificosParaVincular?.Length > 0)
                commands.Add(new LinkerConvEspCmd(requetsDto.IdsConveniosEspecificosParaVincular));

            if (requetsDto.IdsConveniosEspecificosParaDesvincular != null
                && requetsDto.IdsConveniosEspecificosParaDesvincular?.Length > 0)
                commands.Add(new UnlinkConvEspCmd(requetsDto.IdsConveniosEspecificosParaDesvincular));

            foreach (var cmd in commands)
                await cmd.ExecuteAsync(Convenio, _UnitOfWork);

            _UnitOfWork._ConvenioMarcoRepository.ModificarConvenioMarco(Convenio);
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

                _UnitOfWork._ConvenioMarcoRepository.Delete(convenio);
                int rowsAffected = await _UnitOfWork.Save();

                if (rowsAffected == 0)
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

            if (requestDto.InsertEmpresaDto != null)
                commands.Add(new LinkEmpresaToMarcoCmd(requestDto.InsertEmpresaDto));

            if (requestDto.IdsConveniosEspecificosParaVincular != null
                && requestDto.IdsConveniosEspecificosParaVincular?.Length > 0)
                commands.Add(new LinkerConvEspCmd(requestDto.IdsConveniosEspecificosParaVincular));


            foreach (var cmd in commands)
                await cmd.ExecuteAsync(convenio, _UnitOfWork);

            _UnitOfWork._ConvenioMarcoRepository.CreateConvenio(convenio);
            int rowsAffected = await _UnitOfWork.Save();

            if (rowsAffected > 0)
                return Result<ConvenioCreated>.Exito(new ConvenioCreated { Id = convenio.Id, ConvenioType = "marco" });
            else
                return Result<ConvenioCreated>.Error("No se pudo crear el convenio marco.", 500);

        }

        public async Task<ConvenioMarco?> SearchByNumeroConvenio(string Numero)
        {
            var convenio = await _UnitOfWork._ConvenioMarcoRepository.GetByNumeroConvenio(Numero);
            return convenio;
        }

        public async Task<Result<int>> GetIdByNumeroConvenio(string Numero)
        {
            var convenio = await _UnitOfWork._ConvenioMarcoRepository.GetByNumeroConvenio(Numero);
            if (convenio == null) return Result<int>.Error("el convenio que esta buscando no fue encontrado", 404);
            return Result<int>.Exito(convenio.Id);
        }

        public async Task<Result<bool>> DesvincularEspecifico(int IdMarco, int IdEspecifico)
        {
            int[] IdsEspecificos = new int[IdEspecifico];
            var convenioMarco = await _UnitOfWork._ConvenioMarcoReadRepository.GetByidWithConvEspecifico(IdMarco);

            if (convenioMarco == null)
            {
                return Result<bool>.Error("no se pudo encontrar el convenio marco que intenta modificar", 404);
            }

            var cmd = new UnlinkConvEspCmd(IdsEspecificos);

            await cmd.ExecuteAsync(convenioMarco, _UnitOfWork);

            return Result<bool>.Exito(true);
        }

        public async Task<Result<bool>> DesvincularEmpresa(int IdMarco)
        {
            var convenioMarco = await _UnitOfWork._ConvenioMarcoRepository.GetByid(IdMarco);

            if (convenioMarco == null)
            {
                return Result<bool>.Error("no se pudo encontrar el convenio marco que intenta modificar", 404);
            }

            var cmd = new UnlinkEmpresaFromMarcoCmd();

            await cmd.ExecuteAsync(convenioMarco, _UnitOfWork);

            return Result<bool>.Exito(true);
        }
    }
}