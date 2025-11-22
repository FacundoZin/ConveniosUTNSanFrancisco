using APIconvenios.DTOs.Empresa;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class LinkEmpresaToEspecificoCmd : IConvEspCommand
    {
        private readonly InsertEmpresaDto DtoEmpresa;
        public LinkEmpresaToEspecificoCmd(InsertEmpresaDto empresaDto)
        {
            this.DtoEmpresa = empresaDto;
        }

        public async Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {

            if (DtoEmpresa.Id != null)
            {
                Convenio.empresa = await _UnitOfWork._EmpresaRepository.GetById((int)DtoEmpresa.Id);
                Convenio.EmpresaId = DtoEmpresa.Id;
            }
            else
            {
                Convenio.empresa = DtoEmpresa.ToEmpresa();
            }
                

        }
    }
}
