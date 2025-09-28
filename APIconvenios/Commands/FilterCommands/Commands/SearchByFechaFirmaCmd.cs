using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByFechaFirmaCmd : IFilterCommands
    {
        private readonly ByFechaFirmaDto _Dto;
        public SearchByFechaFirmaCmd(ByFechaFirmaDto fechaFirma)
        {
            _Dto = fechaFirma;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if(_Dto.convenioType.Type == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();
                var convenios = await query.Where(c => c.FechaFirmaConvenio == _Dto.FechaInicio).ToListAsync();

                if(convenios == null) return Result<object>.Error("No se encontraron convenios marco con la fecha de firma especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();
                var convenios = await query.Where(c => c.FechaFirmaConvenio == _Dto.FechaInicio).ToListAsync();

                if (convenios == null) return Result<object>.Error("No se encontraron convenios especificos con la fecha de firma especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
        }
    }
}
