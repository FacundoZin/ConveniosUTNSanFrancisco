using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConveniosDocumentManager
    {
        Task<Result<bool>> UploadDocuemnt(IFormFile file, int idconvenio, string conveniotype);
        Task<Result<ConvenioFileContentDto>> DownloadDocument(int idconvenio, string conveniotype); 
    }
}
