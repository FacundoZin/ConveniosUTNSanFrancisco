using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoReadRepository
    {
        Task<InfoConvenioEspeficoDto?> GetConvenioEspecificoCompleto(int id);
        Task<bool> TitleExist(string title);
        Task<bool> TitleExistForUpdate(string title, int idConvenio);

    }
}
