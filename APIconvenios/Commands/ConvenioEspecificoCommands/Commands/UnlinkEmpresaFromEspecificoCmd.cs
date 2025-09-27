using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class UnlinkEmpresaFromEspecificoCmd : IConvEspCommand
    {
        public Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            Convenio.EmpresaId = null;
            return Task.CompletedTask;
        }
    }
}
