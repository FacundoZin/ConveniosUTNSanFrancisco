using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Repositorio
{
    public class ArchivosRepository : IArchivosRepository
    {
        private readonly ApplicationDbContext _context;
        public ArchivosRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteArchivo(int idArchivo)
        {
            var convenio = await _context.ArchivosAdjuntos.FindAsync(idArchivo);

            if (convenio == null) return false;

            _context.ArchivosAdjuntos.Remove(convenio);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ArchivosAdjuntos> GetArchivo(int idArchivo)
        {
            return await _context.ArchivosAdjuntos.FindAsync(idArchivo);
        }


        public async Task<bool> InsertArchivo(ArchivosAdjuntos archivoAdjunto)
        {
            _context.ArchivosAdjuntos.Add(archivoAdjunto);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> NameArchivoExist(string nombreArchivo)
        {
            return await _context.ArchivosAdjuntos.AnyAsync(a => a.NombreArchivo == nombreArchivo);
        }
    }
}
