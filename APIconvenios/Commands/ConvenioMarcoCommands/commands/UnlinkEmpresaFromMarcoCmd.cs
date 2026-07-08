using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarcoCommands.commands
{
    public class UnlinkEmpresaFromMarcoCmd : IConvMarcoCommand
    {
        public Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork)
        {
            convenio.Empresa = null;
            convenio.EmpresaId = null;
            return Task.CompletedTask;
        }
    }
}
