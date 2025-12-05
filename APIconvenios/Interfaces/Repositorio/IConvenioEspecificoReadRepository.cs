using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoReadRepository
    {
        Task<InfoConvenioEspeficoDto?> GetConvenioEspecificoCompleto(int id);
        Task<ConvenioEspecifico?> GetConvenioWithRelations(int id);
        Task<List<ComboBoxConvenioEspecificoDto>> GetAllWithoutTracking();

        Task<Result<object?>> TitleConvenioExist(string title);
        Task<Result<object?>> NumeroConvenioExist(string numeroConvenio);
        Task<Result<object?>> TitleConvenioExistForUpdate(string title, int id);
        Task<Result<object?>> NumeroConvenioExistForUpdate(string? numeroConvenio, int id);

    }
}
