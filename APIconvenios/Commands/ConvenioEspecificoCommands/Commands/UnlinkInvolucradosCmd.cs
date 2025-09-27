using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class UnlinkInvolucradosCmd : IConvEspCommand
    {
        private readonly int[] IdsInvolucrados;
        public UnlinkInvolucradosCmd(int[] IdsInvolucrados)
        {
            this.IdsInvolucrados = IdsInvolucrados;
        }

        public Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            var Involucrados = Convenio.Involucrados!.Where(I => IdsInvolucrados.Contains(I.Id)).ToList();

            foreach (var inv in Involucrados)
                Convenio.Involucrados!.Remove(inv);

            return Task.CompletedTask;
        }
    }
}
