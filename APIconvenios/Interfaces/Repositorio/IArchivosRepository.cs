using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IArchivosRepository
    {
        Task<ArchivosAdjuntos> GetArchivo(int idArchivo);
        Task<bool> DeleteArchivo(int idArchivo);
        Task<bool> InsertArchivo(ArchivosAdjuntos archivoAdjunto);
        Task<bool> NameArchivoExist(string nombreArchivo);
    }
}
