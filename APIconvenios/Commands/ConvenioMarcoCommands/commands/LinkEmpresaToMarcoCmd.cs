using APIconvenios.Commands.ConvenioMarcoCommands;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarco.commands
{
    public class LinkEmpresaToMarcoCmd : IConvMarcoCommand
    {
        private readonly InsertEmpresaDto _Dto;
        public LinkEmpresaToMarcoCmd(InsertEmpresaDto dto)
        {
            _Dto = dto;
        }

        public async Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork)
        {

            if (_Dto.Id != null)
                convenio.EmpresaId = _Dto.Id;
            else
                convenio.Empresa = _Dto.ToEmpresa();
        }
    }
}
