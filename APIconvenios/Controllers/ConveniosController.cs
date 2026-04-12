using APIconvenios.Common;
using APIconvenios.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosController : ControllerBase
    {
        private readonly IConvenioFilterService _conveniosFilterService;
        private readonly IConvenioGetterService _conveniosGetterService;

        public ConveniosController(IConvenioFilterService filterService, IConvenioGetterService getterService)
        {
            _conveniosFilterService = filterService;
            _conveniosGetterService = getterService;
        }

        [HttpPost]
        public async Task<IActionResult> ListarConvenios([FromBody] ConvenioQueryObject queryObject)
        {
            var result = await _conveniosFilterService.ListarConvenios(queryObject);

            if (!result.Exit)
            {
                return StatusCode(result.Errorcode, result.Errormessage);
            }

            if (result is PaginatedResult<object> paginatedResult)
            {
                return Ok(new 
                { 
                    data = paginatedResult.Data, 
                    totalItems = paginatedResult.TotalItems, 
                    totalPages = paginatedResult.TotalPages, 
                    currentPage = paginatedResult.CurrentPage, 
                    pageSize = paginatedResult.PageSize 
                });
            }

            return Ok(result.Data);
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> ObtenerConveniosPorEmpresa(int empresaId)
        {
            var result = await _conveniosGetterService.ListarConveniosPorEmpresa(empresaId);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpGet("involucrado/{involucradoId}")]
        public async Task<IActionResult> ObtenerConveniosPorInvolucrado(int involucradoId)
        {
            var result = await _conveniosGetterService.ListarConveniosPorInvolucrado(involucradoId);

            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }
    }
}
