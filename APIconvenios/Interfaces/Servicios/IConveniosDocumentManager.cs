using APIconvenios.Common;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.Convenios;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConveniosDocumentManager
    {
        Task<Result<bool>> UploadDocuemnt(InsertArchivoDto archivoDto);
        Task<Result<ConvenioFileContentDto>> DownloadDocument(int idconvenio); 
    }
}
