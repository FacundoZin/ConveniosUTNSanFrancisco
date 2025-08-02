using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Filters;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIconvenios.Repositorio
{
    public class ConvenioMarcoReadRepository : IConvenioMarcoReadRepository
    {
        private readonly ApplicationDbContext _Context;

        public ConvenioMarcoReadRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<List<ConvenioMarco>> GetAllConveniosMarcos(Expression<Func<ConvenioMarco, bool>> filtro)
        {
            return await _Context.ConveniosMarcos.Where(filtro).ToListAsync();
        }

        public async Task<List<ConvenioMarco>> GetAllConveniosMarcos(Expression<Func<ConvenioMarco, bool>> filtro, 
            Func<IQueryable<ConvenioMarco>, IOrderedQueryable<ConvenioMarco>>? ordenamiento = null)
        {
            var query = _Context.ConveniosMarcos.Where(filtro);

            if (ordenamiento != null)
                query = ordenamiento(query);

            return await query.ToListAsync();
        }

        public async Task<InfoConvenioMarcoDto?> GetConvenioMarcosCompleto(int id)
        {
            var convenio = await _Context.ConveniosMarcos.Where(c => c.Id == id).Select(c => new InfoConvenioMarcoDto
            {
                Idconvenio = c.Id,
                numeroconvenio = c.numeroconvenio,
                FechaFirmaConvenio = c.FechaFirmaConvenio,
                FechaFin = c.FechaFin,
                ComentarioOpcional = c.ComentarioOpcional,
                empresa = new EmpresaDto
                {
                    Nombre_Empresa = c.Empresa.Nombre,
                    RazonSocial = c.Empresa.RazonSocial,
                    Cuit = c.Empresa.Cuit,
                    Direccion_Empresa = c.Empresa.Direccion,
                    Telefono_Empresa = c.Empresa.Telefono,
                    Email_Empresa = c.Empresa.Email
                },
                ConveniosEspecificos = c.ConveniosEspecificos.Select(ce => new ConvenioEspecificoDto
                {
                    Id = ce.Id,
                    Nombre = ce.Titulo
                }).ToList(),
            }).AsNoTracking().FirstOrDefaultAsync();

            if (convenio == null) return null;

            return convenio;
        }
    }
}
