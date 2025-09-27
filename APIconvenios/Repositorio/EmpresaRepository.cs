using APIconvenios.Data;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}

