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

namespace APIconvenios.Services
{
    public class ConveniosMarcosServices : IConvenioMarcoService
    {
        private readonly _UnitOfWork _UnitOfWork;
        private readonly ILogger<ConveniosMarcosServices> _logger;

        public ConveniosMarcosServices(_UnitOfWork unitOfWork,ILogger<ConveniosMarcosServices> logger, ConvenioQueryObjectValidator queryValidator)
        {
            _UnitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<bool>> ActualizarConvenioMarco(UpdateConvenioMarcoDto convenioActualizado)
        {
            try
            {
                var ConvOriginal = await _UnitOfWork._ConvenioMarcoRepository.GetByid(convenioActualizado.Id);
                if (ConvOriginal == null) return Result<bool>.Error("No se encontró el convenio marco solicitado", 404);

                if(await _UnitOfWork._ConvenioMarcoReadRepository.TitleExist(convenioActualizado.Titulo)) 
                    return Result<bool>.Error("El titulo ingresado coincide con un convenio existente", 409);

                var exit = await _UnitOfWork._ConvenioMarcoRepository.ModificarConvenioMarco(ConvOriginal.UpdateConvenio(convenioActualizado));

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
                var convenio = await _UnitOfWork._ConvenioMarcoRepository.GetByid(id);

                if (convenio == null) return Result<bool>.Error("El convenio que quiere borrar no existe", 404);

                bool exit = await _UnitOfWork._ConvenioMarcoRepository.Delete(convenio);

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

        public async Task<Result<ConvenioCreated>> CargarConvenioMarco(CreateConvenioMarcoDto createConvenioMarcoDto, 
            InsertEmpresaDto EmpresaDto)
        {
            try
            {
                if (await _UnitOfWork._ConvenioMarcoReadRepository.TitleExist(createConvenioMarcoDto.Titulo))
                    return Result<ConvenioCreated>.Error("el nombre de convenio ingresado ya existe", 409);

                var convenio = createConvenioMarcoDto.ConverToConvenioMarco(EmpresaDto);

                _UnitOfWork._ConvenioMarcoRepository.CreateConvenio(convenio);

                bool exit = await _UnitOfWork.Save() > 0;

                if (!exit)
                {
                    _logger.LogError("error al cargar un convenio");
                    return Result<ConvenioCreated>.Error("algo salio mal", 500);
                }

                var result = new ConvenioCreated
                {
                    Id = convenio.Id,
                    ConvenioType = "marco",
                };

                return Result<ConvenioCreated>.Exito(result);
            }
            catch(Exception ex)
            {
                _logger.LogError("error al cargar un convenio");
                return Result<ConvenioCreated>.Error("algo salio mal", 500);
            }
        }
    }
}