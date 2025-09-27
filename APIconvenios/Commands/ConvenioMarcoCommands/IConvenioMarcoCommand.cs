using APIconvenios.Data;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarcoCommands
{
    public interface IConvenioMarcoCommand
    {
        Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork);
    }
}
