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

            if (convenioMarco != null)
            {
                Convenio.ConvenioMarcoId = convenioMarco.Id;
                Convenio.ConvenioMarco = convenioMarco;
                // Herencia automática: si el específico no tiene empresa explícita, hereda la del marco
                if (Convenio.EmpresaId == null && Convenio.empresa == null && convenioMarco.EmpresaId != null)
                {
                    Convenio.EmpresaId = convenioMarco.EmpresaId;
                    Convenio.empresa = convenioMarco.Empresa;
                }
            }
            else
            {
                // Marco no encontrado: no se vincula; log opcional
                Console.WriteLine($"[LinkerConvMarcoCmd] ConvenioMarco id {_idConvMarco} no encontrado.");
            }
        }
    }
}
