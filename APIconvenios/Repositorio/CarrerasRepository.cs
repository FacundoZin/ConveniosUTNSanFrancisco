using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Repositorio
{
    public class CarrerasRepository : ICarreraRepository
    {
        private readonly ApplicationDbContext _context;

        public CarrerasRepository(ApplicationDbContext context)
        {
            _context = context;
        }   
        public async Task<List<Carreras>> GetCarrerasByID(int[] ids)
        {
            return await _context.Carreras.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
