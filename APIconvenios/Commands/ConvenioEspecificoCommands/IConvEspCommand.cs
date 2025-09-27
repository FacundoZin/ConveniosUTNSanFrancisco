using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands
{
    public interface IConvEspCommand
    {
        Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork);
    }
}
