using APIconvenios.DTOs.Archivo;
using APIconvenios.Models;

namespace APIconvenios.Helpers.Mappers
{
    public static class ArchivosMapper
    {
        public static ArchivosAdjuntos ToModel(this InsertArchivoDto archivoDto)
        {
            return new ArchivosAdjuntos
            {
                NombreArchivo = archivoDto.NombreArchivo,
                RutaArchivo = archivoDto.RutaArchivo,
                ConvenioEspecificoId = archivoDto.ConvenioEspecificoId,
                ConvenioMarcoId = archivoDto.ConvenioMarcoId
            };
        }
    }
}
