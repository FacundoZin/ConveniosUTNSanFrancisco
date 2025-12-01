using APIconvenios.Common;
using APIconvenios.Services;
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

        [HttpPost]
        public async Task<IActionResult> ListarConvenios([FromBody] ConvenioQueryObject queryObject)
        {
            var result = await _conveniosFilterService.ListarConvenios(queryObject);

            if (!result.Exit)
            {
                return StatusCode(result.Errorcode, result.Errormessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> ObtenerConveniosPorEmpresa(int empresaId)
        {
            var result = await _conveniosFilterService.ListarConveniosPorEmpresa(empresaId);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }
    }
}
