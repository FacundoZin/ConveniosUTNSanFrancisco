using System.Threading.Tasks;
using APIconvenios.Common;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Helpers.Validators;
using APIconvenios.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;


namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosMarcosController : ControllerBase
    {
        private readonly IConvenioMarcoService _ConvenioService;
        public ConveniosMarcosController(IConvenioMarcoService convenioservice)
        {
            _ConvenioService = convenioservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConveniosMarcos()
        {
            var result = await _ConvenioService.GetAllConveniosMarcos();
            if(!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> VerConvenioMarcoCompleto([FromRoute] int id)
        {
            var result = await _ConvenioService.ObtenerConvenioMarcoCompleto(id);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> BorrarConvenioMarco([FromRoute] int id)
        {
            var result = await _ConvenioService.BorrarConvenioMarco(id);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarConvenioMarco([FromBody] UpdateConvenioMarcoRequetsDto Dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _ConvenioService.ActualizarConvenioMarco(Dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CargarConvenio([FromBody] CargarConvenioMarcoRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _ConvenioService.CargarConvenioMarco(dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Created(string.Empty, result.Data);
        }

        [HttpGet("{numeroConvenio}")]
        public async Task<IActionResult> GetIdConvenioMarco([FromRoute] string numeroConvenio)
        {
            var result = await _ConvenioService.GetIdByNumeroConvenio(numeroConvenio);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpDelete("{idConvenioMarco:int}/empresa")]
        public async Task<IActionResult> DesvincularEmpresa(int idConvenioMarco)
        {
            var result = await _ConvenioService.DesvincularEmpresa(idConvenioMarco);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return NoContent();
        }

        [HttpDelete("{idConvenioMarco:int}/especificos/{idConvenioEspecifico:int}")]
        public async Task<IActionResult> DesvincularConvenioEspecifico(int idConvenioMarco, int idConvenioEspecifico)
        {
            var result = await _ConvenioService.DesvincularEspecifico(idConvenioMarco, idConvenioEspecifico);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return NoContent();
        }

        [HttpGet("archivos/{idConvenio:int}")]
        public async Task<IActionResult> ObtenerArchivosPorConvenioMarco([FromRoute] int idConvenio)
        {
            var result = await _ConvenioService.ObtenerArchivosDeConvenioMarco(idConvenio);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }
    }
}

