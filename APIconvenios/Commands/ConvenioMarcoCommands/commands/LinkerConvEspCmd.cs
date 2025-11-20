using APIconvenios.Commands.ConvenioMarcoCommands;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarco.commands
{
    public class LinkerConvEspCmd : IConvMarcoCommand
    {
        private readonly string _NumeroConvenio;
        public LinkerConvEspCmd(string numeroConvenio)
        {
            _NumeroConvenio = numeroConvenio;
        }

        public async Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork)
        {
            var ConvEspecifico = await _UnitOfWork._ConvenioEspecificoRepository.GetByNumeroConvenio(_NumeroConvenio);

            if (ConvEspecifico != null)
                convenio.ConveniosEspecificos!.Add(ConvEspecifico);

        }
    }
}
