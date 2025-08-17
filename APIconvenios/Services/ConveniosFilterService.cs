using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Helpers.Query;
using APIconvenios.Helpers.Validators;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;
using Microsoft.AspNetCore.Http.Extensions;
using System.Linq.Expressions;

namespace APIconvenios.Services
{
    public class ConveniosFilterService
    {

        private readonly _UnitOfWork _UnitOfWork;
        private readonly ConvenioQueryObjectValidator _QueryValidator;

        public ConveniosFilterService (_UnitOfWork unitOfWork, ConvenioQueryObjectValidator queryvalidator)
        {
            _UnitOfWork = unitOfWork;
            _QueryValidator = queryvalidator;   
        }

        public async Task<Result<ListConveniosDto>> ListarConvenios (ConvenioQueryObject queryObject)
        {
            var Errors = _QueryValidator.Validate(queryObject);

            if(Errors.Count > 0)
                return Result<ListConveniosDto>.Error(string.Join(", ", Errors), 400);

            int SaltoDePaginas = (queryObject.PaginaActual - 1) * queryObject.CantidadResultados;

            var(filtroMarco, ordenamientoMarco) = _QueryBuilder.GenerarParaConvenioMarco(queryObject);
            var(filtroEspecifico, ordenamientoEspecifico) = _QueryBuilder.GenerarParaConvenioEspecifico(queryObject);

            var TaskConvMarco = _UnitOfWork._ConvenioMarcoReadRepository.GetAllConveniosMarcos
                (SaltoDePaginas, queryObject.CantidadResultados, filtroMarco, ordenamientoMarco);

            var TaskConvEspecifico = _UnitOfWork._ConvEspReadRepository.ListarConveniosEspecificos
                (SaltoDePaginas, queryObject.CantidadResultados, filtroEspecifico, ordenamientoEspecifico);

            await Task.WhenAll(TaskConvMarco, TaskConvEspecifico);

            var ConveniosMarcos = await TaskConvMarco;
            var ConveniosEspecificos = await TaskConvEspecifico;  

            return Result<ListConveniosDto>.Exito(new ListConveniosDto 
            {
                ConveniosMarco = ConveniosMarcos.ToDto(),
                ConveniosEspecificos = ConveniosEspecificos.ToDto()
            });
        }
    }
}
