using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Filters;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using APIconvenios.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Helpers.Validators;
using Microsoft.IdentityModel.Tokens;

namespace APIconvenios.Services
{
    public class ConveniosMarcosServices : IConvenioMarcoService
    {
        private readonly IConvenioMarcoRepository _Repo;
        private readonly IConvenioMarcoReadRepository _ReadRepo;
        private readonly ILogger _logger;

        public ConveniosMarcosServices(IConvenioMarcoRepository repo, IConvenioMarcoReadRepository readRepo, ILogger logger)
        {
            _Repo = repo;
            _ReadRepo = readRepo;
            _logger = logger;
        }
        public async Task<Result<bool>> ActualizarConvenioMarco(UpdateConvenioMarcoDto convenioActualizado)
        {
            try
            {
                var ConvOriginal = await _Repo.GetByid(convenioActualizado.Id);
                if (ConvOriginal == null) return Result<bool>.Error("No se encontró el convenio marco solicitado", 404);

                var exit = await _Repo.ModificarConvenioMarco(ConvOriginal.UpdateConvenio(convenioActualizado));
                if (!exit)
                {
                    _logger.LogError($"el convenio marco  {convenioActualizado.Id} no se pudo actualizar");
                    return Result<bool>.Error("Ocurrio un error al actualizar el convenio marco", 500);
                }

                return Result<bool>.Exito(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"el convenio marco  {convenioActualizado.Id} no se pudo actualizar, error: {ex.Message}");
                return Result<bool>.Error("Ocurrio un error al actualizar el convenio marco", 500);
            }
        }

        public async Task<Result<bool>> BorrarConvenioMarco(int id)
        {
            try
            {
                var convenio = await _Repo.GetByid(id);

                if (convenio == null) return Result<bool>.Error("El convenio que quiere borrar no existe", 404);

                bool exit = await _Repo.Delete(convenio);
                if (!exit)
                {
                    _logger.LogError($"el convenio marco  {id} no se pudo eliminar");
                    return Result<bool>.Error("Ocurrio un error al eliminar el convenio marco", 500);
                }

                return Result<bool>.Exito(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"el convenio marco  {id} no se pudo eliminar, error: {ex.Message}");
                return Result<bool>.Error("Ocurrio un error al eliminar el convenio marco", 500);
            }
        }

        public async Task<Result<List<ListaConveniosMarcosDto>>> ListarConveniosMarcos(ConvenioQueryObject queryObject)
        {
            try
            {
                Expression<Func<ConvenioMarco, bool>> filtro = c =>
                (string.IsNullOrEmpty(queryObject.empresa) || c.Empresa.Nombre.Contains(queryObject.empresa)) &&
                (string.IsNullOrEmpty(queryObject.titulo) || c.Titulo.Contains(queryObject.titulo));

                Func<IQueryable<ConvenioMarco>, IOrderedQueryable<ConvenioMarco>>? ordenamiento = null;

                if (queryObject.AntiguedadDescendente)
                {
                    ordenamiento = c => c.OrderByDescending(c => c.FechaFirmaConvenio);
                }
                if (queryObject.AntiguedadAscendente)
                {
                    ordenamiento = c => c.OrderBy(c => c.FechaFirmaConvenio);
                }
                if (queryObject.ProximosAterminar)
                {
                    ordenamiento = c => c.OrderBy(c => c.FechaFin);
                }

                int SaltoDePaginas = (queryObject.PaginaActual - 1) * queryObject.CantidadResultados;

                var convenios = await _ReadRepo.GetAllConveniosMarcos(SaltoDePaginas, queryObject.CantidadResultados, filtro, ordenamiento);

                if (convenios == null)
                    return Result<List<ListaConveniosMarcosDto>>.Error("No hay convenios marcos disponibles", 204);

                return Result<List<ListaConveniosMarcosDto>>.Exito(convenios.ConvertToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"errro en db: {ex.Message}");
                return Result<List<ListaConveniosMarcosDto>>.Error("error en la DB", 503);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"datos no validos al listar convenios {ex.Message}");
                return Result<List<ListaConveniosMarcosDto>>.Error("Estado del sistema no válido", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"excpecion no esperada {ex.Message}");
                return Result<List<ListaConveniosMarcosDto>>.Error("Error inesperado", 500);
            }
        }
        public async Task<Result<InfoConvenioMarcoDto?>> ObtenerConvenioMarcoCompleto(int id)
        {
            try
            {
                var repo = await _ReadRepo.GetConvenioMarcosCompleto(id);

                if (repo == null)
                {
                    return Result<InfoConvenioMarcoDto?>.Error("No se encontró el convenio marco solicitado", 404);
                }

                return Result<InfoConvenioMarcoDto?>.Exito(repo);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"errro en db: {ex.Message}");
                return Result<InfoConvenioMarcoDto?>.Error("error en la DB", 503);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"datos no validos al buscar el convenio {ex.Message}");
                return Result<InfoConvenioMarcoDto?>.Error("Estado del sistema no válido", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"excpecion no esperada {ex.Message}");
                return Result<InfoConvenioMarcoDto?>.Error("Error inesperado", 500);
            }
        }

        public async Task<Result<bool>> CargarConvenioMarco(CreateConvenioMarcoDto createConvenioMarcoDto)
        {
            try
            {
                bool exit = await _Repo.CreateConvenio(createConvenioMarcoDto.ConverToConvenioMarco());

                if (!exit)
                {
                    _logger.LogError("error al cargar un convenio");
                    return Result<bool>.Error("algo salio mal", 500);
                }

                return Result<bool>.Exito(true);
            }
            catch(Exception ex)
            {
                _logger.LogError("error al cargar un convenio");
                return Result<bool>.Error("algo salio mal", 500);
            }


        }
    }
}