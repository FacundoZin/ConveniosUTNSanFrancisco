using APIconvenios.DTOs.Archivo;
using APIconvenios.Models;

namespace APIconvenios.Helpers.Mappers
{
    public static class ArchivosMapper
    {
        public static ArchivosAdjuntos ToModel(this InsertArchivoDto archivoDto, string rutaArchivo)
        {
            return new ArchivosAdjuntos
            {
                NombreArchivo = archivoDto.NombreArchivo,
                ConvenioMarcoId = archivoDto.ConvenioMarcoId,
                ConvenioEspecificoId = archivoDto.ConvenioEspecificoId,
                RutaArchivo = rutaArchivo
            };
        }


    }
}
