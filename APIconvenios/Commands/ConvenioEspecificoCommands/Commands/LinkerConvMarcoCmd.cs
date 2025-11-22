using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class LinkerConvMarcoCmd : IConvEspCommand
    {
        private readonly string _NumeroConvMarco;
        public LinkerConvMarcoCmd(string numeroConvMarco)
        {
            this._NumeroConvMarco = numeroConvMarco;
        }
        public async Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            var convenioMarco = await _UnitOfWork._ConvenioMarcoRepository.GetByNumeroConvenio(_NumeroConvMarco);
            
            if(convenioMarco != null)
                Convenio.ConvenioMarcoId = convenioMarco.Id;
                Convenio.ConvenioMarco = convenioMarco;
        }
    }
}
