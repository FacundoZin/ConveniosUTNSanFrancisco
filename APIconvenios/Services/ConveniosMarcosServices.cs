using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Filters;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using APIconvenios.Helpers;
using APIconvenios.Common;

namespace APIconvenios.Services
{
    public class ConveniosMarcosServices : IConvenioMarcoService
    {
        private readonly IConvenioMarcoRepository _Repo;
        private readonly IConvenioMarcoReadRepository _ReadRepo;
        public ConveniosMarcosServices(IConvenioMarcoRepository repo, IConvenioMarcoReadRepository readRepo)
        {
            _Repo = repo;
            _ReadRepo = readRepo;
        }
        public Task ActualizarConvenioMarco(UpdateConvenioMarcoDto convenioActualizado)
        {
            throw new NotImplementedException();
        }

        public Task BorrarConvenioMarco(int id)
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

                return Result<List<ListaConveniosMarcosDto>>.Exito(convenios.ConvertToList());
            }
            catch (Exception ex)
            {
                return Result<List<ListaConveniosMarcosDto>>.Error("Algo salio mal.", 500);
            }

        }
        public Task<InfoConvenioMarcoDto?> ObtenerConvenioMarcoCompleto(int id)
        {
            throw new NotImplementedException();
        }
    }
}
