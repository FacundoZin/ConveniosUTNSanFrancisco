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
                Nombre = i.Nombre.Trim(),
                Apellido = i.Apellido.Trim(),
                Email = i.Email?.Trim(),
                Telefono = i.Telefono.Trim(),
                Legajo = i.Legajo,
                RolInvolucrado = i.RolInvolucrado,
                IdCarrera = i.IdCarrera
            }).ToList();
        }
    }
}
