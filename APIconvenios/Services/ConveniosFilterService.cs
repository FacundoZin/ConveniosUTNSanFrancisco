using APIconvenios.Commands.FilterCommands.Commands;
using APIconvenios.Common;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Services
{
    public class ConveniosFilterService
    {

        private readonly _UnitOfWork _UnitOfWork;

        public ConveniosFilterService (_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<object>> ListarConvenios (ConvenioQueryObject queryObject)
        {
            if(queryObject.Titulo != null)
            {
                var cmd = new SearchByTitleCmd(queryObject.Titulo);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }else if(queryObject.NumeroResolucion != null)
            {
                var cmd = new SearchByNumeroResolucionCmd(queryObject.NumeroResolucion);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }else if (queryObject.NumeroConvenio != null)
            {
                var cmd = new SearchByNumeroConvenioCmd(queryObject.NumeroConvenio);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.Empresa != null)
            {
                var cmd = new SearchByEmpresaCmd(queryObject.Empresa);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.IsActa != null)
            {
                var cmd = new SearchActaCmd(queryObject.IsActa);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.Refrendado != null)
            {
                var cmd = new SearchByRefrendadoCmd(queryObject.Refrendado);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.Estado != null)
            {
                var cmd = new SearchByEstadoCmd(queryObject.Estado);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.Carrera != null)
            {
                var cmd = new SearchByCarrerasCmd(queryObject.Carrera);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.FechaFirma != null)
            {
                var cmd = new SearchByFechaFirmaCmd(queryObject.FechaFirma);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.FechaFin != null)
            {
                var cmd = new SearchByFechaFinCmd(queryObject.FechaFin);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if (queryObject.AntiguedadDto != null)
            {
                var cmd = new SearchByAntiguedadCmd(queryObject.AntiguedadDto);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }
            else if(queryObject.ProximosAvencer != null)
            {
                var cmd = new SearchProximosAvencerCmd(queryObject.ProximosAvencer);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return result;
            }

            return Result<object>.Error("lo sentimos algo salio mal", 500);
        }
    }
}
