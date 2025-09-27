using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarcoCommands.commands
{
    public class UnlinkConvEspCmd : IConvMarcoCommand
    {
        private readonly int[] _IdsConvEspToUnlink;
        public UnlinkConvEspCmd(int[] idsLink)
        {
            _IdsConvEspToUnlink = idsLink;
        }

        public async Task ExecuteAsync(Models.ConvenioMarco convenio, _UnitOfWork _UnitOfWork)
        {
            var ConvEspecificos = await _UnitOfWork._ConvenioEspecificoRepository.GetConveniosByIds(_IdsConvEspToUnlink);

            foreach (var ConvEsp in ConvEspecificos)
            {
                convenio.ConveniosEspecificos!.Remove(ConvEsp);
            }
        }

    }
}
