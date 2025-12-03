namespace APIconvenios.Interfaces.Repositorio
{
    public interface IInvolucradosRepository
    {
        Task<bool> involucradoExist(string nombre, string apellido);

    }
}
