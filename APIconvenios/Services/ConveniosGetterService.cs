using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrado;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Services
{
    public class ConveniosGetterService : IConvenioGetterService
    {
        private readonly _UnitOfWork _UnitOfWork;

        public ConveniosGetterService(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<EmpresaWithConveniosDto>> ListarConveniosPorEmpresa(int empresaId)
        {
            var empresa = await _UnitOfWork._EmpresaRepository.GetEmpresaWithConvenios(empresaId);
            if (empresa == null)
                return Result<EmpresaWithConveniosDto>.Error("Empresa no encontrada", 404);

            return Result<EmpresaWithConveniosDto>.Exito(new EmpresaWithConveniosDto
            {
                IdEmpresa = empresa.Id,
                NombreEmpresa = empresa.Nombre,
                ConvenioMarco = empresa.ConvenioMarco?.ToDto(),
                conveniosEspecificos = empresa.ConveniosEspecificos.ToDto()
            });
        }

        public async Task<Result<InvolucradosWithConveniosDto>> ListarConveniosPorInvolucrado(int involucradoId)
        {
            var involucrado = await _UnitOfWork._InvolucradosRepository.GetInvolucradoWithConvenios(involucradoId);
            if (involucrado == null)
                return Result<InvolucradosWithConveniosDto>.Error("Involucrado no encontrado", 404);

            return Result<InvolucradosWithConveniosDto>.Exito(new InvolucradosWithConveniosDto
            {
                Id = involucrado.Id,
                Nombre = involucrado.Nombre,
                Apellido = involucrado.Apellido,
                conveniosEspecificos = involucrado.ConveniosEspecificos.ToDto()
            });
        }

        public async Task<Result<UltimosConveniosDto>> ObtenerUltimosConvenios(int cantidad = 5)
        {
            using var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
            using var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

            var queryMarcos = context1.ConveniosMarcos
                .Include(c => c.Empresa)
                .AsNoTracking()
                .OrderByDescending(c => c.FechaFirmaConvenio != null)
                .ThenByDescending(c => c.FechaFirmaConvenio)
                .ThenByDescending(c => c.Id)
                .Take(cantidad);

            var queryEspecificos = context2.ConveniosEspecificos
                .Include(c => c.empresa)
                .AsNoTracking()
                .OrderByDescending(c => c.FechaFirmaConvenio != null)
                .ThenByDescending(c => c.FechaFirmaConvenio)
                .ThenByDescending(c => c.Id)
                .Take(cantidad);

            var taskMarcos = queryMarcos.ToListAsync();
            var taskEspecificos = queryEspecificos.ToListAsync();
            await Task.WhenAll(taskMarcos, taskEspecificos);

            var dto = new UltimosConveniosDto
            {
                ConveniosMarcos = taskMarcos.Result.Select(c => new ConvenioUltimoDto
                {
                    Id = c.Id,
                    Titulo = c.Titulo,
                    ConvenioType = "marco",
                    NombreEmpresa = c.Empresa?.Nombre,
                    Estado = c.Estado
                }).ToList(),
                ConveniosEspecificos = taskEspecificos.Result.Select(c => new ConvenioUltimoDto
                {
                    Id = c.Id,
                    Titulo = c.TituloConvenio,
                    ConvenioType = "especifico",
                    NombreEmpresa = c.empresa?.Nombre,
                    Estado = c.Estado
                }).ToList()
            };

            return Result<UltimosConveniosDto>.Exito(dto);
        }
    }
}
