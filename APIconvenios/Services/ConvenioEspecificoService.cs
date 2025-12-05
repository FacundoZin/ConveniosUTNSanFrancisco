using APIconvenios.Commands.ConvenioEspecificoCommands;
using APIconvenios.Commands.ConvenioEspecificoCommands.Commands;
using APIconvenios.Common;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.UnitOfWork;


namespace APIconvenios.Services
{
    public class ConvenioEspecificoService : IConvenioEspecifcoService
    {
        private readonly _UnitOfWork _UnitOfWork;
        private readonly IValidateConveniosService _ValidateService;
        public ConvenioEspecificoService(_UnitOfWork unitOfWork, IValidateConveniosService validateConveniosService)
        {
            _UnitOfWork = unitOfWork;
            _ValidateService = validateConveniosService;
        }

        public async Task<Result<ConvenioCreated>> CreateConvenioEspecifico(CargarConvenioEspecificoRequestDto Dto)
        {
            var resultValidation = await _ValidateService.ValidateCargaConvenioEspecifico(Dto);

            if (!resultValidation.Exit)
                return Result<ConvenioCreated>.Error(resultValidation.Errormessage, resultValidation.Errorcode);

            var Convenio = Dto.InsertConvenioDto.UploadData();

            List<IConvEspCommand> Commnands = new List<IConvEspCommand>();

            if (Dto.InsertInvolucradosDto != null)
                Commnands.Add(new InsertInvolucradorToConvEspCmd(Dto.InsertInvolucradosDto));

            if (Dto.idCarreras != null)
                Commnands.Add(new LinkCarrerasCmd(Dto.idCarreras));

            if (Dto.InsertEmpresaDto != null)
                Commnands.Add(new LinkEmpresaToEspecificoCmd(Dto.InsertEmpresaDto));

            if (Dto.numeroConvenioMarcoVinculado != null)
                Commnands.Add(new LinkerConvMarcoCmd(Dto.numeroConvenioMarcoVinculado));

            foreach (var command in Commnands)
                await command.ExecuteAsync(Convenio, _UnitOfWork);

            _UnitOfWork._ConvenioEspecificoRepository.CreateConvenio(Convenio);
            int rowsAffected = await _UnitOfWork.Save();

            if (rowsAffected > 0)
                return Result<ConvenioCreated>.Exito(new ConvenioCreated { Id = Convenio.Id, ConvenioType = "especifico" });
            else
                return Result<ConvenioCreated>.Error("No se pudo crear el convenio especifico.", 500);
        }

        public async Task<Result<object?>> DeleteConvenioEspecifico(int id)
        {
            try
            {
                var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(id);

                if (convenio == null)
                    return Result<object?>.Error($"el convenio no existe", 404);

                _UnitOfWork._ConvenioEspecificoRepository.Delete(convenio);
                int rowsAffected = await _UnitOfWork.Save();

                if (rowsAffected == 0)
                {
                    return Result<object?>.Error($"Error al eliminar el convenio especifico", 500);
                }

                return Result<object?>.Exito(null);
            }
            catch (Exception ex)
            {
                return Result<object?>.Error($"Error al eliminar el convenio especifico", 500);
            }
        }


        public async Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoRequestDto Dto)
        {
            var resultValidation = await _ValidateService.ValidateUpdateConvenioEspecifico(Dto);

            if (!resultValidation.Exit)
                return Result<object?>.Error(resultValidation.Errormessage, resultValidation.Errorcode);


            var Convenio = await _UnitOfWork._ConvEspReadRepository.GetConvenioWithRelations(Dto.UpdateConvenioDto.Id);

            if (Convenio == null) return Result<object?>.Error("El convenio que quiere actualizar no existe", 404);

            
            var commands = new List<IConvEspCommand>();

            Convenio.UpdateConvenio(Dto.UpdateConvenioDto);

            if (Dto.InsertInvolucradosDtos != null && Dto.InsertInvolucradosDtos.Any())
                commands.Add(new InsertInvolucradorToConvEspCmd(Dto.InsertInvolucradosDtos));

            if (Dto.idCarreras != null && Dto.idCarreras.Length > 0)
                commands.Add(new LinkCarrerasCmd(Dto.idCarreras));

            if (Dto.DesvincularEmpresa)
                commands.Add(new UnlinkEmpresaFromEspecificoCmd());

            if (Dto.InsertEmpresaDto != null)
                commands.Add(new LinkEmpresaToEspecificoCmd(Dto.InsertEmpresaDto));

            if (Dto.IdsInvolucraodsEliminados != null && Dto.IdsInvolucraodsEliminados.Any())
                commands.Add(new UnlinkInvolucradosCmd(Dto.IdsInvolucraodsEliminados));

            if (Dto.DesvincularConvenioMarco)
                commands.Add(new UnlinkConvMarcoCmd());

            if (Dto.numeroConvenioMarcoVinculado != null)
                commands.Add(new LinkerConvMarcoCmd(Dto.numeroConvenioMarcoVinculado));

            foreach (var command in commands)
                await command.ExecuteAsync(Convenio, _UnitOfWork);

            _UnitOfWork._ConvenioEspecificoRepository.ModificarConvenioEspecifico(Convenio);
            int rowsAffected = await _UnitOfWork.Save();


            if (rowsAffected > 0)
                return Result<object?>.Exito(new ConvenioCreated { Id = Convenio.Id, ConvenioType = "especifico" });
            else
                return Result<object?>.Error("No se pudo actualizar el convenio especifico.", 500);
        }

        public async Task<Result<int>> GetIdByNumeroConvenio(string NumeroConvenio)
        {
            var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByNumeroConvenio(NumeroConvenio);
            if (convenio == null) return Result<int>.Error("el convenio que esta buscando no fue encontrado", 404);

            return Result<int>.Exito(convenio.Id);
        }

        public async Task<Result<InfoConvenioEspeficoDto>> ObtenerConvenioEspecificoCompleto(int id)
        {
            var convenio = await _UnitOfWork._ConvEspReadRepository.GetConvenioEspecificoCompleto(id);

            if (convenio == null) return Result<InfoConvenioEspeficoDto>.Error("El convenio especifico no existe", 404);


            return Result<InfoConvenioEspeficoDto>.Exito(convenio);
        }

        public async Task<Result<bool>> DesvincularEmpresa(int IdConvEspecifico)
        {
            var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(IdConvEspecifico);

            if (convenio == null)
            {
                return Result<bool>.Error("no se pudo encontrar el convenio especifico que intenta modificar", 404);
            }
            var cmd = new UnlinkEmpresaFromEspecificoCmd();

            await cmd.ExecuteAsync(convenio, _UnitOfWork);

            return Result<bool>.Exito(true);
        }

        public async Task<Result<bool>> DesvincularMarco(int IdConvEspecifico)
        {
            var convenio = await _UnitOfWork._ConvenioEspecificoRepository.GetByid(IdConvEspecifico);

            if (convenio == null)
            {
                return Result<bool>.Error("no se pudo encontrar el convenio especifico que intenta modificar", 404);
            }

            var cmd = new UnlinkConvMarcoCmd();

            await cmd.ExecuteAsync(convenio, _UnitOfWork);

            return Result<bool>.Exito(true);
        }

        public async Task<Result<List<viewArchivoDto>>> ObtenerArchivosDeConvenioEspecifico(int idConvenio)
        {
            try
            {
                var archivos = await _UnitOfWork._ArchivosRepository.GetArchivosDeConvenioEspecifico(idConvenio);

                var dto = archivos.Select(a => new viewArchivoDto
                {
                    IdArchivo = a.Id,
                    NombreArchivo = a.NombreArchivo
                }).ToList();

                return Result<List<viewArchivoDto>>.Exito(dto);
            }
            catch
            {
                return Result<List<viewArchivoDto>>.Error("ocurrio un error al obtener los archivos", 500);
            }
        }

        public async Task<Result<List<ComboBoxConvenioEspecificoDto>>> GetAllConveniosEspecificos()
        {
            try
            {
                var convenios = await _UnitOfWork._ConvEspReadRepository.GetAllWithoutTracking();

                return Result<List<ComboBoxConvenioEspecificoDto>>.Exito(convenios);
            }
            catch(Exception ex)
            {
                return Result<List<ComboBoxConvenioEspecificoDto>>.Error("Ocurrio un error al obtener los convenios especificos", 500);
            }
        }
    }

}
