using APIconvenios.Commands.ConvenioMarcoCommands;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarco.commands
{
    public class LinkerConvEspCmd : IConvMarcoCommand
    {
        private readonly int[] _IdsConvEspToLink;
        public LinkerConvEspCmd(int[] idsLink)
        {
            _IdsConvEspToLink = idsLink;
        }

        public async Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork)
        {
            var ConvEspecificos = await _UnitOfWork._ConvenioEspecificoRepository.GetConveniosByIds(_IdsConvEspToLink);

            foreach (var ConvEsp in ConvEspecificos)
            {
                convenio.ConveniosEspecificos!.Add(ConvEsp);
            }
        }
    }
}
