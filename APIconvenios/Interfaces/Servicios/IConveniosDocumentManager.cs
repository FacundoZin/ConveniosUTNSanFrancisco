using APIconvenios.Common;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConveniosDocumentManager
    {
        Task<Result<viewArchivoDto>> UploadDocuemnt(InsertArchivoDto archivoDto);
        Task<Result<ConvenioFileContentDto>> DownloadDocument(int idconvenio);
        Task<Result<bool>> DeleteDocument(int idconvenio);
    }
}
