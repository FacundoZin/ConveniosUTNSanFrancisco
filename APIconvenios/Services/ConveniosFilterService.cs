using APIconvenios.Commands.FilterCommands.Commands;
using APIconvenios.Common;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Services
{
    public class ConveniosFilterService
    {

        private readonly _UnitOfWork _UnitOfWork;

        public ConveniosFilterService(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<object>> ListarConvenios(ConvenioQueryObject queryObject)
        {
            if (queryObject.ByTitulo != null)
            {
                var cmd = new SearchByTitleCmd(queryObject.ByTitulo);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByNumeroResolucion != null)
            {
                var cmd = new SearchByNumeroResolucionCmd(queryObject.ByNumeroResolucion);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByNumeroConvenio != null)
            {
                var cmd = new SearchByNumeroConvenioCmd(queryObject.ByNumeroConvenio);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByEmpresa != null)
            {
                var cmd = new SearchByEmpresaCmd(queryObject.ByEmpresa);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByIsActa != null)
            {
                var cmd = new SearchActaCmd(queryObject.ByIsActa);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByIsRefrendado != null)
            {
                var cmd = new SearchByRefrendadoCmd(queryObject.ByIsRefrendado);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByEstado != null)
            {
                var cmd = new SearchByEstadoCmd(queryObject.ByEstado);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByCarrera != null)
            {
                var cmd = new SearchByCarrerasCmd(queryObject.ByCarrera);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByFechaFirma != null)
            {
                var cmd = new SearchByFechaFirmaCmd(queryObject.ByFechaFirma);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByFechaFin != null)
            {
                var cmd = new SearchByFechaFinCmd(queryObject.ByFechaFin);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByAntiguedadDto != null)
            {
                var cmd = new SearchByAntiguedadCmd(queryObject.ByAntiguedadDto);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByProximosAvencer != null)
            {
                var cmd = new SearchProximosAvencerCmd(queryObject.ByProximosAvencer);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.ByMes != null)
            {
                var cmd = new SearchByMesCmd(queryObject.ByMes);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByAño != null)
            {
                var cmd = new SearchByAñoCmd(queryObject.ByAño);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.ByDesdeHastaDto != null)
            {
                var cmd = new SearchByDesdeHastaCmd(queryObject.ByDesdeHastaDto);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.CountFirmadosByMesDto != null)
            {
                var cmd = new CountConvFirmadosByMesCmd(queryObject.CountFirmadosByMesDto);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.countFirmadosByRangoDto != null)
            {
                var cmd = new CountConvFirmadosByRangoCmd(queryObject.countFirmadosByRangoDto);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }


            return Result<object>.Error("Porfavor seleccione un filtro", 400);
        }
    }
}
