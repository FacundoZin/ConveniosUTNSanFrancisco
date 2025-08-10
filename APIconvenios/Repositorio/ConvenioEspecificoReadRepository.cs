using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIconvenios.Repositorio
{
    public class ConvenioEspecificoReadRepository : IConvenioEspecificoReadRepository
    {
        private readonly ApplicationDbContext _context;

        public ConvenioEspecificoReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InfoConvenioEspeficoDto> GetConvenioEspecificoCompleto(int id)
        {
            var convenio = await _context.ConveniosEspecificos.Where(c => c.Id == id).Select(c => new InfoConvenioEspeficoDto
            {
                Id = c.Id,
                ConvenioMarcoId = c.ConvenioMarcoId,
                numeroconvenio = c.ConvenioMarco.numeroconvenio,
                Titulo = c.Titulo,
                FechaFirmaConvenio = c.FechaFirmaConvenio,
                FechaFinConvenio = c.FechaFinConvenio,
                FechaInicioActividades = c.FechaInicioActividades,
                ComentarioOpcional = c.ComentarioOpcional,
                Involucrados = c.Involucrados.Select(i => new InvolucradosDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    Email = i.Email,
                    Telefono = i.Telefono,
                    Legajo = i.Legajo,
                    RolInvolucrado = i.RolInvolucrado.ToString(),
                }).ToList()
            }).AsNoTracking().FirstAsync();

            return convenio;
        }

        public async Task<List<ConvenioEspecificoDto>> ListarConveniosEspecificos(int SaltoPaginas, int CantidadPaginas, 
            Expression<Func<ConvenioEspecifico, bool>> filtro, Func<IQueryable<ConvenioEspecifico>, 
                IOrderedQueryable<ConvenioEspecifico>>? ordenamiento = null)
        {
            var query = _context.ConveniosEspecificos.Where(filtro);

            if (ordenamiento != null)
                query = ordenamiento(query);

            query.Skip(SaltoPaginas);
            query.Take(CantidadPaginas);

            var convenios = await query.ToListAsync();

            return convenios.ToDto();
        }
    }
}
