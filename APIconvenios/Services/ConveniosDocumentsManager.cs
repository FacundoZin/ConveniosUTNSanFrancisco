using APIconvenios.Common;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Services
{
    public class ConveniosDocumentsManager : IConveniosDocumentManager
    {
        private readonly _UnitOfWork _UnitOfWork;
        private readonly string directorioArchivos = @"C:\conveniosdocuments\";
        public ConveniosDocumentsManager(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> DeleteDocument(int idDocumento)
        {
            var archivo = await _UnitOfWork._ArchivosRepository.GetArchivo(idDocumento);
            if (archivo == null)
                return Result<bool>.Error("No se encontró el archivo en la base de datos", 404);

            try
            {
                if (File.Exists(archivo.RutaArchivo))
                {
                    File.Delete(archivo.RutaArchivo);
                }
                else
                {
                    Console.WriteLine($"El archivo físico no existe en la ruta: {archivo.RutaArchivo}");
                    return Result<bool>.Error("No se encontró el archivo en el servidor", 404);
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error al eliminar el archivo físico: {ex.Message}", 500);
            }

            var exit = await _UnitOfWork._ArchivosRepository.DeleteArchivo(archivo);

            if (!exit)
                return Result<bool>.Error("El archivo no pudo ser eliminado de la base de datos", 500);

            return Result<bool>.Exito(true);
        }

        public async Task<Result<ConvenioFileContentDto>> DownloadDocument(int IdDocumento)
        {
            try
            {
                var archivo = await _UnitOfWork._ArchivosRepository.GetArchivo(IdDocumento);

                if (!File.Exists(archivo.RutaArchivo))
                    return Result<ConvenioFileContentDto>.Error("El archivo no se encuentra en el servidor", 404);


                byte[] fileBytes = await File.ReadAllBytesAsync(archivo.RutaArchivo);

                var fileContent = new ConvenioFileContentDto
                {
                    Bytes = fileBytes,
                    FileName = archivo.NombreArchivo,
                    ContentType = archivo.ContentType 
                };

                return Result<ConvenioFileContentDto>.Exito(fileContent);
            }
            catch (Exception ex)
            {
                return Result<ConvenioFileContentDto>.Error($"Lo sentimos, algo salio mal", 500);
            }
        }

        public async Task<Result<viewArchivoDto>> UploadDocuemnt(InsertArchivoDto archivoDto)
        {
            try
            {
                if (archivoDto.file == null || archivoDto.file.Length == 0)
                    return Result<viewArchivoDto>.Error("No se seleccionó ningún archivo para subir", 400);


                if (await _UnitOfWork._ArchivosRepository.NameArchivoExist(archivoDto.NombreArchivo))
                    return Result<viewArchivoDto>.Error($"Ya existe un archivo con el nombre {archivoDto.NombreArchivo}, " +
                        $"porfavor cambie el nombre para que el documento sea unico ", 400);


                if (!Directory.Exists(directorioArchivos))
                {
                    try
                    {
                        Directory.CreateDirectory(directorioArchivos);
                    }
                    catch (Exception ex)
                    {
                        return Result<viewArchivoDto>.Error("Error al crear la carpeta de destino", 500);
                    }
                }

                
                var extensionArchivo = Path.GetExtension(archivoDto.file.FileName); 
                var nombreFinal = archivoDto.NombreArchivo+extensionArchivo;

                string rutaCompleta = Path.Combine(directorioArchivos, nombreFinal);

                var ArchivoCreado = await FileUploadTransaction(archivoDto.ToModel(rutaCompleta,extensionArchivo), archivoDto.file, rutaCompleta);

                if (ArchivoCreado == null)
                    return Result<viewArchivoDto>.Error("se produjo un error inesperado al cargar el documento en el servidor...",
                        500);

                return Result<viewArchivoDto>.Exito(ArchivoCreado.ToViewDto());
            }
            catch
            {
                return Result<viewArchivoDto>.Error("Error al cargar el documento del convenio", 500);
            }
        }


        private async Task<ArchivosAdjuntos?> FileUploadTransaction(ArchivosAdjuntos Archivo, IFormFile file, string RutaCompleta)
        {
            using (var transaction = await _UnitOfWork.BeginTransaction())
            {
                await _UnitOfWork._ArchivosRepository.InsertArchivo(Archivo);

                try
                {
                    using (var stream = new FileStream(RutaCompleta, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    await transaction.CommitAsync();

                    return Archivo;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }
    }
}
