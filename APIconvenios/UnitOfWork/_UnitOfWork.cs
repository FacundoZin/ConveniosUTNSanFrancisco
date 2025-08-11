using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;

namespace APIconvenios.UnitOfWork
{
    public class _UnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        public IConvenioEspecificoRepository _ConvenioEspecificoRepository { get; }
        public IInvolucradosRepository _InvolucradosRepository { get; }
        public IConvenioEspecificoReadRepository _ConvEspReadRepository { get; }

        public _UnitOfWork(ApplicationDbContext applicationDbContext, IConvenioEspecificoRepository convenioEspecificorepo,
            IInvolucradosRepository involucradosRepository, IConvenioEspecificoReadRepository convespreadrepo)
        {
            _Context = applicationDbContext;
            _ConvenioEspecificoRepository = convenioEspecificorepo;
            _InvolucradosRepository = involucradosRepository;
            _ConvEspReadRepository = convespreadrepo;
        }   

        public async Task<int> Save ()
            => await _Context.SaveChangesAsync();

        public void MarkAsExisting<TEntity>(TEntity entity) where TEntity : class
            => _Context.Attach(entity);

    }
}
