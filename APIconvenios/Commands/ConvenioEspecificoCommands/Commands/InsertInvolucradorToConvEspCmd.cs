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
                var existsInMemory = Convenio.Involucrados != null && Convenio.Involucrados.Any(e =>
                    e.Nombre.ToLower().Trim() == involucrado.Nombre.ToLower().Trim() &&
                    e.Apellido.ToLower().Trim() == involucrado.Apellido.ToLower().Trim() &&
                    (e.Telefono ?? string.Empty).ToLower().Trim() == (involucrado.Telefono ?? string.Empty).ToLower().Trim());

                if (existsInMemory) continue;

                Convenio.Involucrados!.Add(involucrado);
            }

            return Task.CompletedTask;
        }
    }
}
