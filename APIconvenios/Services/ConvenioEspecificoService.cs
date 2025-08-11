using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Helpers.Validators;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;
using System;
using System.Linq.Expressions;

namespace APIconvenios.Services
{
    public class ConvenioEspecificoService : IConvenioEspecifcoService
    {
        private readonly _UnitOfWork _UnitOfWork;
        private readonly ILogger _Logger;
        private readonly ConvenioQueryObjectValidator _QueryVallidator;
        public ConvenioEspecificoService(_UnitOfWork unitOfWork, ILogger logger, ConvenioQueryObjectValidator queryvalidator)
        {
            _UnitOfWork = unitOfWork;
            _Logger = logger;
            _QueryVallidator = queryvalidator;
        }

        public async Task<Result<object?>> CreateConvenioEspecifico(InsertConvenioEspecificoDto DtoConvenio,
            List<InsertInvolucradosDto> DtoInvolucrados)
        {
            try
            {
                var Involucrados = DtoInvolucrados.ToInvolucrados();

                foreach (var inv in Involucrados)
                {
                    if (inv.Id != 0)
                    {
                        _UnitOfWork.MarkAsExisting(inv);
                    }
                }

                _UnitOfWork._ConvenioEspecificoRepository.CreateConvenio(new ConvenioEspecifico
                {

                    numeroconvenio = DtoConvenio.numeroconvenio,
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

            bool exit = await _UnitOfWork._ConvenioEspecificoRepository.ModificarConvenioEspecifico(ConvenioOriginal.UpdateConvenio(Dto));

            if (!exit)
            {
                _Logger.LogError($"Error al editar el convenio especifico con id {Dto.Id}");
                return Result<object?>.Error($"Error al editar el convenio especifico", 500);
            }

            return Result<Object?>.Exito(null);
        }

        public async Task<Result<List<ConvenioEspecificoDto>>> ListarConveniosEspecificos(ConvenioQueryObject queryObject)
        {
            var errores = _QueryVallidator.Validate(queryObject);
            if (errores.Count > 0)
                return Result<List<ConvenioEspecificoDto>>.Error(string.Join(", ", errores), 400);

            Expression<Func<ConvenioEspecifico, bool>> filtro = c =>
            (string.IsNullOrEmpty(queryObject.empresa) || c.ConvenioMarco.Empresa.Nombre.Contains(queryObject.empresa)) &&
            (string.IsNullOrEmpty(queryObject.titulo) || c.Titulo.Contains(queryObject.titulo));

            Func<IQueryable<ConvenioEspecifico>, IOrderedQueryable<ConvenioEspecifico>>? ordenamiento = null;

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
                ordenamiento = c => c.OrderBy(c => c.FechaFinConvenio);
            }

            int SaltoDePaginas = (queryObject.PaginaActual - 1) * queryObject.CantidadResultados;

            var convenios = await _UnitOfWork._ConvEspReadRepository.ListarConveniosEspecificos(SaltoDePaginas, queryObject.CantidadResultados, filtro, ordenamiento);

            if (convenios == null)
                return Result<List<ConvenioEspecificoDto>>.Error("No hay convenios marcos disponibles", 204);

            return Result<List<ConvenioEspecificoDto>>.Exito(convenios);
        }

        public async Task<Result<InfoConvenioEspeficoDto>> ObtenerConvenioEspecificoCompleto(int id)
        {
            var convenio = await _UnitOfWork._ConvEspReadRepository.GetConvenioEspecificoCompleto(id);

            if(convenio == null) return Result<InfoConvenioEspeficoDto>.Error("El convenio especifico no existe", 404);

            return Result<InfoConvenioEspeficoDto>.Exito(convenio);
        }
    }
}
