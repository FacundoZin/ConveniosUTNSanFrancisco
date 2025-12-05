using APIconvenios.Common;
using APIconvenios.Data;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace APIconvenios.Repositorio
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ApplicationDbContext _Context;

        public EmpresaRepository(ApplicationDbContext context)
        {
            _Context = context;
        }


        public async Task<List<Models.Empresa>> GetAll()
        {
            var empresas = await _Context.Empresas.ToListAsync();
            return empresas;
        }

        public async Task<Models.Empresa> GetById(int id)
        {
            var empresa = await _Context.Empresas.FirstOrDefaultAsync(e => e.Id == id);
            return empresa;
        }

        public async Task<int> Add(Empresa empresa)
        {
            await _Context.Empresas.AddAsync(empresa);
            return empresa.Id;
        }

        public async Task<Empresa?> GetEmpresaWithConvenios(int id)
        {
            return await _Context.Empresas
                .Include(e => e.ConveniosEspecificos)
                .Include(e => e.ConvenioMarco)
                .FirstOrDefaultAsync(e => e.Id == id);

        }

        public async Task<Result<object?>> NameEmpresaExist(string Name)
        {
            bool result = await _Context.Empresas
                .AnyAsync(c => c.Nombre.ToLower() == Name.ToLower());

            if(result) return Result<object?>.Error("Ya existe una empresa con ese nombre", 400);
            return Result<object?>.Exito(null);

        }

        public async Task<Result<object?>> NameEmpresaExistForUpdate(string Name, int idEmpresa)
        {
            bool result = await _Context.Empresas
               .AnyAsync(c => c.Nombre.ToLower() == Name.ToLower() && c.Id != idEmpresa);

            if (result) return Result<object?>.Error("Ya existe una empresa con ese nombre", 400);
            return Result<object?>.Exito(null);
        }

        public async Task EditEmpresaDto(int idEmpresa, EditEmpresaDto dto)
        {
            var empresa = await _Context.Empresas.FindAsync(idEmpresa);

            if (empresa == null) throw new Exception("Empresa no encontrada");

            empresa.Nombre = dto.Nombre;
            empresa.Email = dto.Email;
            empresa.Direccion = dto.Direccion;
            empresa.RazonSocial = dto.RazonSocial;
            empresa.Telefono = dto.Telefono;
            empresa.Cuit = dto.Telefono;

            await _Context.SaveChangesAsync();
        }
    }
}

