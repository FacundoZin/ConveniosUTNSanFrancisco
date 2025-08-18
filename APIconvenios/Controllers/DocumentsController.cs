using APIconvenios.DTOs.Convenios;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IConveniosDocumentManager _ConvenioDocumentsManager;
        public DocumentsController(IConveniosDocumentManager documentsManager)
        {
            _ConvenioDocumentsManager = documentsManager;
        }   

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadDocument([FromForm] UploadConvenioDocumentDto Dto )
        {
            var result = await _ConvenioDocumentsManager.UploadDocuemnt(Dto.File, Dto.IdConvenio, Dto.ConvenioType);

            if(!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);   

            return Ok();
        }

        [HttpGet("{idConvenio}/{convenioType}")]
        public async Task<IActionResult> DownloadDocument(int idConvenio, string convenioType)
        {
            var result = await _ConvenioDocumentsManager.DownloadDocument(idConvenio, convenioType);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            var fileContent = result.Data;
            return File(fileContent.Bytes, fileContent.ContentType, fileContent.FileName);
        }   
    }
}
