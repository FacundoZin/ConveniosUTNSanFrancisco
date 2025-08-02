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
        public Task ActualizarConvenioMarco(UpdateConvenioMarcoDto convenioActualizado)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> BorrarConvenioMarco(int id)
        {
            throw new NotImplementedException();
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


                var convenios = await _ReadRepo.GetAllConveniosMarcos(filtro, ordenamiento);
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
    }


    
}