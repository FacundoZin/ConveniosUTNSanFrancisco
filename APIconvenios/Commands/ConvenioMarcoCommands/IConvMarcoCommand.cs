using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarcoCommands
{
    public interface IConvMarcoCommand
    {
        Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork);
    }
}
