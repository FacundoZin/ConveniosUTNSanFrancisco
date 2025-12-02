using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioMarcoReadRepository
    {
        Task<List<ComboBoxConvenioMarcoDto>> GetAllWithoutTracking();
        Task<InfoConvenioMarcoDto?> GetConvenioMarcosCompleto(int id);
        Task<bool> TitleExist(string Title);
        Task<bool> TitleExistForUpdate(string title, int idConvenio);
        Task<ConvenioMarco?> GetByidWithConvEspecifico(int id);
    }
}
