using APIconvenios.Interfaces.Servicios;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Services
{
    public class ConveniosStateService : IConveniosStateService
    {
        private readonly _UnitOfWork _unitOfWork;
        public ConveniosStateService(_UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task MarkConveniosAsFinished(DateOnly finishDate)
        {
            var TaskGetMarcos = _unitOfWork._ConvenioMarcoRepository.SetStateToFinish(finishDate);
            var TaskGetEspecificos = _unitOfWork._ConvenioEspecificoRepository.SetStateTofinish(finishDate);

            await Task.WhenAll(TaskGetMarcos, TaskGetEspecificos);  
        }
    }
}
