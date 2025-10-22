using APIconvenios.Common;
using APIconvenios.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosController : ControllerBase
    {
        private readonly ConveniosFilterService _conveniosFilterService;
        public ConveniosController(ConveniosFilterService filterService)
        {
            _conveniosFilterService = filterService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarConvenios([FromQuery] ConvenioQueryObject queryObject)
        {
            var result = await _conveniosFilterService.ListarConvenios(queryObject);

            if (!result.Exit)
            {
                return StatusCode(result.Errorcode, result.Errormessage);
            }

            return Ok(result.Data);
        }
        
        
    }
}
