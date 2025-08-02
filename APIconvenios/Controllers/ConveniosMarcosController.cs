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

        public async Task<IActionResult> ListaConvenios([FromQuery] ConvenioQueryObject queryObject)
        {
            _QueryValidator.Validate(queryObject);
            if(_QueryValidator.Errors.Count != 0) return BadRequest(_QueryValidator.Errors);


            var result = await _ConvenioService.ListarConveniosMarcos(queryObject);
            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result);
        }
    }
}
