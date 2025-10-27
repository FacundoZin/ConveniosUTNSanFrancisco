using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosEspecificosController : ControllerBase
    {
        private readonly IConvenioEspecifcoService _ConvenioEspecifcoService;
        public ConveniosEspecificosController(IConvenioEspecifcoService convenioEspecifcoService)
        {
            _ConvenioEspecifcoService = convenioEspecifcoService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerConvenioEspecificoCompleto(int id)
        {
            var result = await _ConvenioEspecifcoService.ObtenerConvenioEspecificoCompleto(id);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CargarConvenio([FromBody] CargarConvenioEspecificoRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest("los datos ingresados no son validos");

            var result = await _ConvenioEspecifcoService.CreateConvenioEspecifico(dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Created(string.Empty, result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> EditarConvenio([FromBody] UpdateConvenioEspecificoRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest("los datos ingresados no son correctos");

            var result = await _ConvenioEspecifcoService.EditarConvenioEspecifico(dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ConvenioEspecifcoService.DeleteConvenioEspecifico(id);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpGet("{numeroConvenio}")]
        public async Task<IActionResult> GetIdConvenioEspecifico([FromRoute] string numeroConvenio)
        {
            var result = await _ConvenioEspecifcoService.GetIdByNumeroConvenio(numeroConvenio);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }


        [HttpDelete("{idConvenioEspecifico:int}/empresa")]
        public async Task<IActionResult> DesvincularEmpresa(int idConvenioEspecifico)
        {
            var result = await _ConvenioEspecifcoService.DesvincularEmpresa(idConvenioEspecifico);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return NoContent();
        }

        [HttpDelete("{idConvenioEspecifico:int}/marco")]
        public async Task<IActionResult> DesvincularConvenioMarco(int idConvenioEspecifico)
        {
            var result = await _ConvenioEspecifcoService.DesvincularMarco(idConvenioEspecifico);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return NoContent();
        }

        [HttpGet("archivos/{idConvenio:int}")]
        public async Task<IActionResult> ObtenerArchivosPorConvenioMarco([FromRoute] int idConvenio)
        {
            var result = await _ConvenioEspecifcoService.ObtenerArchivosDeConvenioEspecifico(idConvenio);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }
    }
}
