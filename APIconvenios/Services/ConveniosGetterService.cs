using APIconvenios.Common;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrado;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.UnitOfWork;

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
                Nombre = involucrado.Nombre,
                Apellido = involucrado.Apellido,
                conveniosEspecificos = involucrado.ConveniosEspecificos.ToDto()
            });
        }
    }
}
