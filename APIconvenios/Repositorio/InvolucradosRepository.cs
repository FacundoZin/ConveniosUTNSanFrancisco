using APIconvenios.Data;
using APIconvenios.DTOs.Involucrado;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Repositorio
{
    public class InvolucradosRepository : IInvolucradosRepository
    {
        private readonly ApplicationDbContext _context;

        public InvolucradosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Involucrados>> GetAllInvolucraods()
        {
            return await _context.Involucrados
                .Select(i => new Involucrados
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Apellido = i.Apellido
                })
                .ToListAsync();
        }

        public async Task<List<Involucrados>> GetInvolucradosByCarrera(int IdCarrera)
        {
            return await _context.Involucrados
                .Where(i => i.IdCarrera == IdCarrera)
                .Include(i => i.Carrera)
                .AsNoTracking()
                .ToListAsync();    
        }

        public async Task<List<Involucrados>> GetAvailableForConvenio(int idConvenio)
        {
            return await _context.Involucrados
                .Where(i => !i.ConveniosEspecificos.Any(c => c.Id == idConvenio))
                .ToListAsync();
        }

        public async Task<List<Involucrados>> GetInvolucradosByIds(int[] ids)
        {
            return await _context.Involucrados
                .Where(i => ids.Contains(i.Id))
                .ToListAsync();
        }

        public async Task<bool> involucradoExist(string nombre, string apellido)
        {
            return await _context.Involucrados
                .AnyAsync(i => i.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && i.Apellido.ToLower() == apellido.ToLower());
        }
    }
}
