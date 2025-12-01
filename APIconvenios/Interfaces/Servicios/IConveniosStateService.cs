namespace APIconvenios.Interfaces.Servicios
{
    public interface IConveniosStateService
    {
        Task MarkConveniosAsFinished(DateOnly finishDate);
    }
}
