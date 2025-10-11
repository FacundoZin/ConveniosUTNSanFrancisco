using APIconvenios.Common.Enums;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.Helpers.Mappers
{
    public static class ConvenioMarcoMappers
    {
        public static List<ConvenioMarcoDto> ToDto(this List<ConvenioMarco> convenios)
        {
            List<ConvenioMarcoDto> conveniosDto = new List<ConvenioMarcoDto>();

            foreach (var Convenio in convenios)
            {
                conveniosDto.Add(new ConvenioMarcoDto
                {
                    Id = Convenio.Id,
                    Titulo = Convenio.Titulo,
                    numeroconvenio = Convenio.numeroconvenio,
                    NombreEmpresa = Convenio.Empresa.Nombre,
                    FechaFirmaConvenio = Convenio.FechaFirmaConvenio,
                    FechaFin = Convenio.FechaFin,
                    Estado = Convenio.Estado,
                    Refrendado = Convenio.Refrendado
                });
            }

            return conveniosDto;
        }

        public static ConvenioMarco UpdateData(this ConvenioMarco convenio, UpdateConvenioMarcoDto convenioActualizado)
        {
            convenio.numeroconvenio = convenioActualizado.numeroconvenio;
            convenio.Titulo = convenioActualizado.Titulo;
            convenio.FechaFirmaConvenio = convenioActualizado.FechaFirmaConvenio;
            convenio.FechaFin = convenioActualizado.FechaFin;
            convenio.ComentarioOpcional = convenioActualizado.ComentarioOpcional;
            convenio.Estado = convenioActualizado.Estado;
            convenio.NumeroResolucion = convenioActualizado.NumeroResolucion;
            convenio.Refrendado = convenioActualizado.Refrendado;

            return convenio;
        }



        public static void UploadData(this ConvenioMarco convenio, InsertConvenioMarcoDto convenioDto)
        {
            convenio.numeroconvenio = convenioDto.numeroconvenio;
            convenio.Titulo = convenioDto.Titulo;
            convenio.FechaFirmaConvenio = convenioDto.FechaFirmaConvenio;
            convenio.FechaFin = convenioDto.FechaFin;
            convenio.ComentarioOpcional = convenioDto.ComentarioOpcional;
            convenio.Estado = convenioDto.Estado;
            convenio.NumeroResolucion = convenioDto.NumeroResolucion;
            convenio.Refrendado = convenioDto.Refrendado;
        }

        public static InfoConvenioMarcoDto ToFullInfo(this ConvenioMarco convenio)
        {
            return new InfoConvenioMarcoDto
            {
                Id = convenio.Id,
                numeroconvenio = convenio.numeroconvenio,
                Titulo = convenio.Titulo,
                FechaFirmaConvenio = convenio.FechaFirmaConvenio,
                FechaFin = convenio.FechaFin,
                ComentarioOpcional = convenio.ComentarioOpcional,
                Estado = convenio.Estado,
                NumeroResolucion = convenio.NumeroResolucion,
                Refrendado = convenio.Refrendado,
                empresa = convenio.Empresa != null
                ? new EmpresaDto
                {
                    Id = convenio.Empresa.Id,
                    Nombre_Empresa = convenio.Empresa.Nombre,
                    RazonSocial = convenio.Empresa.RazonSocial,
                    Cuit = convenio.Empresa.Cuit,
                    Direccion_Empresa = convenio.Empresa.Direccion,
                    Telefono_Empresa = convenio.Empresa.Telefono,
                    Email_Empresa = convenio.Empresa.Email
                } : null,

                ConveniosEspecificos = convenio.ConveniosEspecificos?.Select(ce => new ConvenioEspecificoDto
                {
                    Id = ce.Id,
                    Titulo = ce.TituloConvenio,
                    numeroconvenio = ce.numeroconvenio,
                    FechaFirmaConvenio = ce.FechaFirmaConvenio,
                    FechaInicioActividades = ce.FechaInicioActividades,
                    FechaFin = ce.FechaFinConvenio,
                    Estado = ce.Estado,
                    Refrendado = ce.Refrendado,
                    EsActa = ce.EsActa,
                    ConvenioType = "especifico"
                }).ToList(),

                ArchivosAdjuntos = convenio.ArchivosAdjuntos?.Select(aa => new viewArchivoDto
                {
                    IdArchivo = aa.Id,
                    NombreArchivo = aa.NombreArchivo,
                }).ToList()

            };
        }
    }
}
