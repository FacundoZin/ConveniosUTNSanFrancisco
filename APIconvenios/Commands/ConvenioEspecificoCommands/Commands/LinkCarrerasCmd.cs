using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class LinkCarrerasCmd : IConvEspCommand
    {
        private readonly int[] IdsCarreras;
        public LinkCarrerasCmd(int[] idsCarreras)
        {
            this.IdsCarreras = idsCarreras;
        }
        public async Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            Convenio.CarrerasInvolucradas = await _UnitOfWork._CarreraRepository.GetCarrerasByID(IdsCarreras);
        }
    }
}
