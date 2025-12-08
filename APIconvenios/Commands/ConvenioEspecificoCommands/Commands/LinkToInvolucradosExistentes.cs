using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class LinkToInvolucradosExistentes : IConvEspCommand
    {
        private readonly int[] _idsInvolucrados;
        public LinkToInvolucradosExistentes(int[] idsInvolucrados)
        {
            _idsInvolucrados = idsInvolucrados;
        }
        public async Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            var involucrados = await _UnitOfWork._InvolucradosRepository.GetInvolucradosByIds(_idsInvolucrados);

            foreach(var inv in involucrados)
            {
                Convenio.Involucrados!.Add(inv);
            }
        }
    }
}
