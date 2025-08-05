using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Filters;
using APIconvenios.Helpers.Validators;
using APIconvenios.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosMarcosController : ControllerBase
    {
        private readonly IConvenioMarcoService _ConvenioService;
        private readonly ConvenioQueryObjectValidator _QueryValidator;
        public ConveniosMarcosController(IConvenioMarcoService convenioservice, ConvenioQueryObjectValidator validator)
        {
            _ConvenioService = convenioservice;
            _QueryValidator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> ListaConvenios([FromQuery] ConvenioQueryObject queryObject)
        {
            var result = await _ConvenioService.ListarConveniosMarcos(queryObject);
            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> VerConvenioMarcoCompleto([FromRoute] int id)
        {
            var result = await _ConvenioService.ObtenerConvenioMarcoCompleto(id);

            if(!result.Exit) return StatusCode(result.Errorcode,result.Errormessage);    

            return Ok(result.Data);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> BorrarConvenioMarco([FromRoute] int id)
        {
            var result = await _ConvenioService.BorrarConvenioMarco(id);

            if(!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data); 
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarConvenioMarco([FromBody] UpdateConvenioMarcoDto Dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _ConvenioService.ActualizarConvenioMarco(Dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CargarConvenio([FromBody] CreateConvenioMarcoDto Dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _ConvenioService.CargarConvenioMarco(Dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }
    }
}
