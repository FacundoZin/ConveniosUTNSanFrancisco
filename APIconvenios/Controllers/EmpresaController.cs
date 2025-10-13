using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using APIconvenios.DTOs.Empresa;
using APIconvenios.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly _UnitOfWork _UnitOfWork;
        public EmpresaController(_UnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEmpresas()
        {
            var empresas = await _UnitOfWork._EmpresaRepository.GetAll();

            var dto = empresas.Select(e => new ComboBoxEmpresasDto
            {
                IdEmpresa = e.Id,
                NombreEmpresa = e.Nombre
            });

            return Ok(dto);
        }
    }
}