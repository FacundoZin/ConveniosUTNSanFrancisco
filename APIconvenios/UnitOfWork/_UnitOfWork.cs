using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;

namespace APIconvenios.UnitOfWork
{
    public class _UnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        public IConvenioEspecificoRepository _ConvenioEspecificoRepository { get; }
        public IInvolucradosRepository _InvolucradosRepository { get; }
        
        public _UnitOfWork(ApplicationDbContext applicationDbContext, IConvenioEspecificoRepository convenioEspecificorepo,
            IInvolucradosRepository involucradosRepository)
        {
            _Context = applicationDbContext;
            _ConvenioEspecificoRepository = convenioEspecificorepo;
            _InvolucradosRepository = involucradosRepository;
        }   

        public async Task<int> Save ()
            => await _Context.SaveChangesAsync();

        public void MarkAsExisting<TEntity>(TEntity entity) where TEntity : class
            => _Context.Attach(entity);

    }
}
