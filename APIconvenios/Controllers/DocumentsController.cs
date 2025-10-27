using APIconvenios.DTOs.Archivo;
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
        public async Task<IActionResult> UploadDocument([FromForm] InsertArchivoDto Dto)
        {
            var result = await _ConvenioDocumentsManager.UploadDocuemnt(Dto);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpGet("{idDocumento:int}")]
        public async Task<IActionResult> DownloadDocument(int idDocumento)
        {
            var result = await _ConvenioDocumentsManager.DownloadDocument(idDocumento);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            var fileContent = result.Data;
            return File(fileContent.Bytes, fileContent.ContentType, fileContent.FileName);
        }

        [HttpDelete("{idDocumento:int}")]
        public async Task<IActionResult> DeleteDocument(int idDocumento)
        {
            var result = await _ConvenioDocumentsManager.DeleteDocument(idDocumento);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return NoContent();
        }
    }
}
