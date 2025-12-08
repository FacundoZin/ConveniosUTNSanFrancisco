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

        public async Task<bool> involucradoExist(string nombre, string apellido)
        {
            return await _context.Involucrados
                .AnyAsync(i => i.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && i.Apellido.ToLower() == apellido.ToLower());
        }
    }
}
