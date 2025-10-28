using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IArchivosRepository
    {
        Task<ArchivosAdjuntos> GetArchivo(int idArchivo);
        Task<bool> DeleteArchivo(ArchivosAdjuntos archivo);
        Task<ArchivosAdjuntos> InsertArchivo(ArchivosAdjuntos archivoAdjunto);
        Task<bool> NameArchivoExist(string nombreArchivo);
        Task<List<ArchivosAdjuntos>> GetArchivosDeConvenioMarco(int IdMarco);
        Task<List<ArchivosAdjuntos>> GetArchivosDeConvenioEspecifico(int IdEspecifico);
    }
}
