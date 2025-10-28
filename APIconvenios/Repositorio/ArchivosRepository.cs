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
        public async Task<bool> DeleteArchivo(ArchivosAdjuntos archivo)
        {
            _context.ArchivosAdjuntos.Remove(archivo);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ArchivosAdjuntos> GetArchivo(int idArchivo)
        {
            return await _context.ArchivosAdjuntos.FindAsync(idArchivo);
        }

        public Task<List<ArchivosAdjuntos>> GetArchivosDeConvenioEspecifico(int IdEspecifico)
        {
            var archivos = _context.ArchivosAdjuntos.Where(a => a.ConvenioEspecificoId == IdEspecifico).ToListAsync();
            return archivos;
        }

        public Task<List<ArchivosAdjuntos>> GetArchivosDeConvenioMarco(int IdMarco)
        {
            var archivos = _context.ArchivosAdjuntos.Where(a => a.ConvenioMarcoId == IdMarco).ToListAsync();
            return archivos;
        }
        public async Task<ArchivosAdjuntos> InsertArchivo(ArchivosAdjuntos archivoAdjunto)
        {
            _context.ArchivosAdjuntos.Add(archivoAdjunto);
            await _context.SaveChangesAsync();
            return archivoAdjunto;
        }

        public async Task<bool> NameArchivoExist(string nombreArchivo)
        {
            return await _context.ArchivosAdjuntos.AnyAsync(a => a.NombreArchivo == nombreArchivo);
        }
    }
}
