using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;

namespace APIconvenios.Helpers.Mappers
{
    public static class InvolucradosMapper
    {
        public static List<Involucrados> ToInvolucrados(this List<InsertInvolucradosDto> involucradosDtos)
        {
            return involucradosDtos.Select(i => new Involucrados
            {
                Nombre = i.Nombre,
                Apellido = i.Apellido,
                Email = i.Email,
                Telefono = i.Telefono,
                Legajo = i.Legajo,
                RolInvolucrado = i.RolInvolucrado,
                IdCarrera = i.IdCarrera
            }).ToList();
        }
    }
}
