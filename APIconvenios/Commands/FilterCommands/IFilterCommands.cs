using APIconvenios.Common;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.FilterCommands
{
    public interface IFilterCommands
    {
        Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork);
    }
}
