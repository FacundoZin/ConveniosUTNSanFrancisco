using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarcoCommands.commands
{
    public class UnlinkEmpresaFromMarcoCmd : IConvMarcoCommand
    {
        public async Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork)
        {
            convenio.Empresa = null;
            convenio.EmpresaId = null;

            await _UnitOfWork.Save();
        }
    }
}
