using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioMarcoReadRepository
    {
        Task<List<ComboBoxConvenioMarcoDto>> GetAllWithoutTracking();
        Task<InfoConvenioMarcoDto?> GetConvenioMarcosCompleto(int id);
        Task<Result<object?>> TitleConvenioExist(string Title);
        Task<Result<object?>> TitleConvenioExistForUpdate(string title, int id);
        Task<ConvenioMarco?> GetByidWithConvEspecifico(int id);
        Task<Result<object?>> NumeroConvenioExist(string numeroConvenio);
        Task<Result<object?>> NumeroConvenioExistForUpdate(string numeroConvenio, int id);
    }
}
