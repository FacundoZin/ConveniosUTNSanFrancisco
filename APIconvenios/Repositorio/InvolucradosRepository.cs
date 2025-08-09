using APIconvenios.Data;
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
        public async Task Create(List<Involucrados> involucrados)
        {
            await _context.AddRangeAsync(involucrados);
        }

        public async Task<List<Involucrados>> GetInvolucrados(List<int> Ids)
        {
            var involucrados =  await _context.Involucrados
                .Where(i => Ids.Contains(i.Id))
                .ToListAsync();

            return involucrados;
        }
    }
}
