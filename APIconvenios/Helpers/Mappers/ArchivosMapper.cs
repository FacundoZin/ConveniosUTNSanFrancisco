using APIconvenios.DTOs.Archivo;
using APIconvenios.Models;

namespace APIconvenios.Helpers.Mappers
{
    public static class ArchivosMapper
    {
        public static ArchivosAdjuntos ToModel(this InsertArchivoDto archivoDto, string rutaArchivo, string extensionArchivo)
        {
            return new ArchivosAdjuntos
            {
                NombreArchivo = archivoDto.NombreArchivo+extensionArchivo,
                ConvenioMarcoId = archivoDto.ConvenioMarcoId,
                ConvenioEspecificoId = archivoDto.ConvenioEspecificoId,
                RutaArchivo = rutaArchivo,
                ContentType = archivoDto.file.ContentType
            };
        }

        public static viewArchivoDto ToViewDto(this ArchivosAdjuntos archivo)
        {
            return new viewArchivoDto
            {
                IdArchivo = archivo.Id,
                NombreArchivo = archivo.NombreArchivo
            };
        }
    }
}
