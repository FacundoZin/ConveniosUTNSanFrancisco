using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.Helpers.Mappers
{
    public static class EmpresaMapper
    {
        public static Empresa ToEmpresa(this InsertEmpresaDto insertDto)
        {
            if(insertDto.Id == null)
            {
                return new Empresa
                {
                    Nombre = insertDto.Nombre,
                    Cuit = insertDto.Cuit,
                    Direccion = insertDto.Direccion,
                    Telefono = insertDto.Telefono,
                    Email = insertDto.Email,
                    RazonSocial = insertDto.RazonSocial
                };
            }
            else
            {
                return new Empresa
                {
                    Id = insertDto.Id.Value,
                    Nombre = insertDto.Nombre,
                    Cuit = insertDto.Cuit,
                    Direccion = insertDto.Direccion,
                    Telefono = insertDto.Telefono,
                    Email = insertDto.Email,
                    RazonSocial = insertDto.RazonSocial
                };
            }

        }
    }
}
