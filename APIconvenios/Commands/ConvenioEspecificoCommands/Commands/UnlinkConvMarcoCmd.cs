using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class UnlinkConvMarcoCmd : IConvEspCommand
    {
        public Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            Convenio.ConvenioMarcoId = null;

            return Task.CompletedTask;
        }
    }
}
