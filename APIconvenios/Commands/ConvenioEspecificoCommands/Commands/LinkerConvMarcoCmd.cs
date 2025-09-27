using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class LinkerConvMarcoCmd : IConvEspCommand
    {
        private readonly int idConvMarco;
        public LinkerConvMarcoCmd(int idConvMarco)
        {
            this.idConvMarco = idConvMarco;
        }
        public Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            Convenio.ConvenioMarcoId = idConvMarco;
            return Task.CompletedTask;
        }
    }
}
