using APIconvenios.Common.Enums;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;
using System.Runtime.CompilerServices;

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
                    NombreEmpresa = Convenio.Empresa?.Nombre,
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

        public static ConvenioMarco ConverToConvenioMarco(this InsertConvenioMarcoDto ConvenioDto, InsertEmpresaDto empresaDto)
        {
            return new ConvenioMarco
            {
                numeroconvenio = ConvenioDto.numeroconvenio,
                Titulo = ConvenioDto.Titulo,
                FechaFirmaConvenio = ConvenioDto.FechaFirmaConvenio,
                FechaFin = ConvenioDto.FechaFin,
                ComentarioOpcional = ConvenioDto.ComentarioOpcional,


                Empresa = new Empresa
                {
                    Nombre = empresaDto.Nombre,
                    RazonSocial = empresaDto.RazonSocial,
                    Cuit = empresaDto.Cuit,
                    Direccion = empresaDto.Direccion,
                    Telefono = empresaDto.Telefono,
                    Email = empresaDto.Email
                }
            };
        }

        public static void UploadData(this ConvenioMarco convenio, InsertConvenioMarcoDto convenioDto)
        {
            convenio.numeroconvenio = convenioDto.numeroconvenio;
            convenio.Titulo = convenioDto.Titulo;
            convenio.FechaFirmaConvenio = convenioDto.FechaFirmaConvenio;
            convenio.FechaFin = convenioDto.FechaFin;
            convenio.ComentarioOpcional = convenioDto.ComentarioOpcional;
            convenio.Estado = (convenioDto.Estado;
            convenio.NumeroResolucion = convenioDto.NumeroResolucion;
            convenio.Refrendado = convenioDto.Refrendado;
        }
    }
}
