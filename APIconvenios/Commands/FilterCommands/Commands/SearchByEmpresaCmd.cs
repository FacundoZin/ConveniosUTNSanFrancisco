using APIconvenios.Common;
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
            if(_Dto.convenioType.Type == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                var convenios = await query.Where(c => c.Empresa != null && c.Empresa.Nombre.ToLower().Contains(
                    _Dto.EmpresaName.ToLower())).Include(c => c.Empresa).ToListAsync();


                if (convenios.Count == 0) return Result<object>.Error("No se encontraron convenios con la empresa especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var convenios = await query.Where(c => c.empresa != null && c.empresa.Nombre.ToLower().Contains(
                    _Dto.EmpresaName.ToLower())).Include(c => c.empresa).ToListAsync();

                if (convenios.Count == 0) return Result<object>.Error("No se encontraron convenios con la empresa especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
        }
    }
}
