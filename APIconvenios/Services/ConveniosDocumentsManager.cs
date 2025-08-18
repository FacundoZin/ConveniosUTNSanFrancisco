using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Services
{
    public class ConveniosDocumentsManager : IConveniosDocumentManager
    {
        private readonly _UnitOfWork _UnitOfWork;
        public ConveniosDocumentsManager(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<ConvenioFileContentDto>> DownloadDocument(int idconvenio, string conveniotype)
        {
            string rutaArchivo = string.Empty;

            try
            {
                if (conveniotype == "marco")
                {
                    var convenio = await _UnitOfWork._ConvenioMarcoRepository.GetByid(idconvenio);
                    if (convenio == null || string.IsNullOrEmpty(convenio.RutaArchivo))
                    {
                        return Result<ConvenioFileContentDto>.Error("Este convenio no tiene un documento asociado", 404);
                    }
                    rutaArchivo = convenio.RutaArchivo;
                }
                else if (conveniotype == "especifico")
                {
                    var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(idconvenio);
                    if (convenio == null || string.IsNullOrEmpty(convenio.RutaArchivo))
                    {
                        return Result<ConvenioFileContentDto>.Error("Este convenio no tiene un docuumento asociado", 404);
                    }
                    rutaArchivo = convenio.RutaArchivo;
                }
                else
                {
                    return Result<ConvenioFileContentDto>.Error("Tipo de convenio no válido", 400);
                }

                if (!File.Exists(rutaArchivo))
                    return Result<ConvenioFileContentDto>.Error("El archivo no se encuentra en el servidor", 404);
                

                // Lee los bytes del archivo del disco.
                byte[] fileBytes = await File.ReadAllBytesAsync(rutaArchivo);
                string nombreArchivo = Path.GetFileName(rutaArchivo);

                var fileContent = new ConvenioFileContentDto
                {
                    Bytes = fileBytes,
                    FileName = nombreArchivo,
                    ContentType = "application/octet-stream" // Tipo de contenido genérico
                };

                return Result<ConvenioFileContentDto>.Exito(fileContent);
            }
            catch (Exception ex)
            {
                return Result<ConvenioFileContentDto>.Error($"Lo sentimos, algo salio mal", 500);
            }
        }

        public async Task<Result<bool>> UploadDocuemnt(IFormFile file, int idconvenio, string conveniotype)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Result<bool>.Error("No se seleccionó ningún archivo para subir", 400);


                string uploadsFolder = @"C:\conveniosdocuments\";

                if (!Directory.Exists(uploadsFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    catch (Exception ex)
                    {
                        return Result<bool>.Error("Error al crear la carpeta de destino", 500);
                    }
                }

                string nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string rutaCompleta = Path.Combine(uploadsFolder, nombreUnico);

                await FileUploadTransaction(file, rutaCompleta, idconvenio, conveniotype);

                return Result<bool>.Exito(true);
            }
            catch
            {
                return Result<bool>.Error("Error al cargar el documento del convenio", 500);
            }
        }


        private async Task FileUploadTransaction(IFormFile file, string rutaCompleta, int id, string convenioType)
        {
            using (var transaction = await _UnitOfWork.BeginTransaction())
            {
                if (convenioType == "marco")
                {
                    var convenio = await _UnitOfWork._ConvenioMarcoRepository.GetByid(id);
                    convenio.RutaArchivo = rutaCompleta;
                }
                else
                {
                    var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(id);
                    convenio.RutaArchivo = rutaCompleta;
                }

                await _UnitOfWork.Save(); 

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                await transaction.CommitAsync();
            }
        }
    }
}
