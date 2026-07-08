using APIconvenios.Common;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly _UnitOfWork _UnitOfWork;

        public EmpresaService(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<List<ComboBoxEmpresasDto>>> ListarEmpresasPaginado(int pagina, int cantidad)
        {
            var query = _UnitOfWork._EmpresaRepository.GetAllAsQueryable();
            
            var totalItems = await query.CountAsync();
            
            var empresas = await query
                .Skip((pagina - 1) * cantidad)
                .Take(cantidad)
                .Select(e => new ComboBoxEmpresasDto
                {
                    IdEmpresa = e.Id,
                    NombreEmpresa = e.Nombre,
                })
                .ToListAsync();

            return PaginatedResult<List<ComboBoxEmpresasDto>>.ExitoPaginado(empresas, totalItems, pagina, cantidad);
        }

        public async Task<Result<List<ComboBoxEmpresasDto>>> ListarTodasLasEmpresas()
        {
            var empresas = await _UnitOfWork._EmpresaRepository.GetAll();
            var dto = empresas.Select(e => new ComboBoxEmpresasDto
            {
                IdEmpresa = e.Id,
                NombreEmpresa = e.Nombre,
            }).ToList();

            return Result<List<ComboBoxEmpresasDto>>.Exito(dto);
        }

        public async Task<Result<int>> CrearEmpresa(InsertEmpresaDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return Result<int>.Error("El nombre de la empresa es requerido.", 400);

            var exists = await _UnitOfWork._EmpresaRepository.NameEmpresaExist(dto.Nombre);
            if (!exists.Exit)
            {
                return Result<int>.Error(exists.Errormessage, 400);
            }

            var nuevaEmpresa = new Empresa
            {
                Nombre = dto.Nombre,
                RazonSocial = dto.RazonSocial,
                Cuit = dto.Cuit,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Email = dto.Email
            };

            await _UnitOfWork._EmpresaRepository.Add(nuevaEmpresa);
            await _UnitOfWork.Save();

            return Result<int>.Exito(nuevaEmpresa.Id);
        }

        public async Task<Result<bool>> EditarEmpresa(int idEmpresa, EditEmpresaDto dto)
        {
            await _UnitOfWork._EmpresaRepository.EditEmpresaDto(idEmpresa, dto);
            return Result<bool>.Exito(true);
        }

        public async Task<Result<EmpresaDto>> ObtenerEmpresaPorId(int id)
        {
            var empresa = await _UnitOfWork._EmpresaRepository.GetById(id);
            if (empresa == null)
                return Result<EmpresaDto>.Error("Empresa no encontrada", 404);

            var dto = new EmpresaDto
            {
                Id = empresa.Id,
                Nombre_Empresa = empresa.Nombre,
                RazonSocial = empresa.RazonSocial,
                Cuit = empresa.Cuit,
                Direccion_Empresa = empresa.Direccion,
                Telefono_Empresa = empresa.Telefono,
                Email_Empresa = empresa.Email
            };

            return Result<EmpresaDto>.Exito(dto);
        }
    }
}
