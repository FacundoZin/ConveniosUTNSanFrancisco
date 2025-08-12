using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;

namespace APIconvenios.UnitOfWork
{
    public class _UnitOfWork
    {

        private readonly ApplicationDbContext _Context;


        public IConvenioEspecificoRepository _ConvenioEspecificoRepository { get; }
        public IConvenioEspecificoReadRepository _ConvEspReadRepository { get; }
        public IConvenioMarcoRepository _ConvenioMarcoRepository { get; }
        public IConvenioMarcoReadRepository _ConvenioMarcoReadRepository { get; }



        public _UnitOfWork(ApplicationDbContext applicationDbContext, IConvenioEspecificoRepository convenioEspecificorepo,
            IConvenioEspecificoReadRepository convespreadrepo,
            IConvenioMarcoRepository convmarcRepository, IConvenioMarcoReadRepository convmarcReadRepo)
        {
            _Context = applicationDbContext;
            _ConvenioEspecificoRepository = convenioEspecificorepo;
            _ConvEspReadRepository = convespreadrepo;
            _ConvenioMarcoRepository = convmarcRepository;
            _ConvenioMarcoReadRepository = convmarcReadRepo;
        }   

        public async Task<int> Save ()
            => await _Context.SaveChangesAsync();

        public void MarkAsExisting<TEntity>(TEntity entity) where TEntity : class
            => _Context.Attach(entity);

    }
}
