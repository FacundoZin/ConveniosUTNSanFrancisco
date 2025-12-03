using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByEmpresaCmd : IFilterCommands
    {
        private readonly ByEmpresaDto _Dto;
        public SearchByEmpresaCmd(ByEmpresaDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering();

                var convenios = await query.Where(c => c.Empresa != null && c.Empresa.Nombre.ToLower().Contains(
                    _Dto.EmpresaName.ToLower())).ToListAsync();


                if (convenios.Count == 0) return Result<object>.Error("No se encontraron convenios con la empresa especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else if(_Dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering();

                var convenios = await query.Where(c => c.empresa != null && c.empresa.Nombre.ToLower().Contains(
                    _Dto.EmpresaName.ToLower())).ToListAsync();

                if (convenios.Count == 0) return Result<object>.Error("No se encontraron convenios con la empresa especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var task1 = context1.ConveniosEspecificos.Where(c => c.empresa != null && c.empresa.Nombre.ToLower().Contains(
                    _Dto.EmpresaName.ToLower())).ToListAsync();

                var task2 = context2.ConveniosMarcos.Where(c => c.Empresa != null && c.Empresa.Nombre.ToLower().Contains(
                    _Dto.EmpresaName.ToLower())).ToListAsync();

                await Task.WhenAll(task1, task2);

                var conveniosEspecificos = await task1;
                var conveniosMarcos = await task2;

                if (conveniosMarcos.Count == 0 && conveniosEspecificos.Count == 0)
                    return Result<object>.Error("no hay convenios que coincidan con la busqueda", 404);


                var Data = new ListConveniosDto
                {
                    conveniosMarcos = conveniosMarcos.ToDto(),
                    convenioEspecificos = conveniosEspecificos.ToDto(),
                };


                return Result<object>.Exito(Data);
            }
        }
    }
}
