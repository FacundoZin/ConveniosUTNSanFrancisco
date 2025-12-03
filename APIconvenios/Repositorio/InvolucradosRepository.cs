using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;
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
        public async Task<bool> involucradoExist(string nombre, string apellido)
        {
            return await _context.Involucrados.AnyAsync(i => i.Nombre == nombre && i.Apellido == apellido);
        }
    }
}
