using APIconvenios.DTOs.Involucrados;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class InsertInvolucradorToConvEspCmd : IConvEspCommand
    {
        private readonly List<InsertInvolucradosDto> involucradosDtos;

        public InsertInvolucradorToConvEspCmd(List<InsertInvolucradosDto> Incolucrados)
        {
            this.involucradosDtos = Incolucrados;
        }

        public Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            var involucrados = this.involucradosDtos.ToInvolucrados();

            foreach (var involucrado in involucrados)
            {
                Convenio.Involucrados.Add(involucrado);
            }

            return Task.CompletedTask;
        }
    }
}
