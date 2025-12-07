using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioEspecificoCommands.Commands
{
    public class LinkerConvMarcoCmd : IConvEspCommand
    {
        private readonly int _idConvMarco;
        public LinkerConvMarcoCmd(int idConvMarco)
        {
            this._idConvMarco = idConvMarco;
        }
        public async Task ExecuteAsync(ConvenioEspecifico Convenio, _UnitOfWork _UnitOfWork)
        {
            var convenioMarco = await _UnitOfWork._ConvenioMarcoRepository.GetByid(_idConvMarco);
            
            if(convenioMarco != null)
                Convenio.ConvenioMarcoId = convenioMarco.Id;
                Convenio.ConvenioMarco = convenioMarco;
        }
    }
}
