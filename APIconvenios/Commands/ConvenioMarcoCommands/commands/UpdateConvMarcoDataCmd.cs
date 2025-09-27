using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.ConvenioMarcoCommands.commands
{
    public class UpdateConvMarcoDataCmd : IConvMarcoCommand
    {
        private readonly UpdateConvenioMarcoDto _dto;

        public UpdateConvMarcoDataCmd(UpdateConvenioMarcoDto dto)
        {
            _dto = dto;
        }

        public async Task ExecuteAsync(APIconvenios.Models.ConvenioMarco convenio, _UnitOfWork _unitOfWork)
        {
            convenio.UpdateData(_dto);
        }
    }
}
