using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Repositorio;
using Microsoft.EntityFrameworkCore.Storage;

namespace APIconvenios.UnitOfWork
{
    public class _UnitOfWork
    {

        private readonly ApplicationDbContext _Context;


        public IConvenioEspecificoRepository _ConvenioEspecificoRepository { get; }
        public IConvenioEspecificoReadRepository _ConvEspReadRepository { get; }
        public IConvenioMarcoRepository _ConvenioMarcoRepository { get; }
        public IConvenioMarcoReadRepository _ConvenioMarcoReadRepository { get; }
        public IEmpresaRepository _EmpresaRepository { get; }
        public ICarreraRepository _CarreraRepository { get; } 
        public IArchivosRepository _ArchivosRepository { get; }


        public _UnitOfWork(ApplicationDbContext applicationDbContext, IConvenioEspecificoRepository convenioEspecificorepo,
            IConvenioEspecificoReadRepository convespreadrepo,
            IConvenioMarcoRepository convmarcRepository, IConvenioMarcoReadRepository convmarcReadRepo, 
            IEmpresaRepository empresaRepository,ICarreraRepository carreraRepository,
            IArchivosRepository archivosRepository)
        {
            _Context = applicationDbContext;
            _ConvenioEspecificoRepository = convenioEspecificorepo;
            _ConvEspReadRepository = convespreadrepo;
            _ConvenioMarcoRepository = convmarcRepository;
            _ConvenioMarcoReadRepository = convmarcReadRepo;
            _EmpresaRepository = empresaRepository;
            _CarreraRepository = carreraRepository;
            _ArchivosRepository = archivosRepository;
        }   

        public async Task<int> Save ()
            => await _Context.SaveChangesAsync();

        public void MarkAsExisting<TEntity>(TEntity entity) where TEntity : class
            => _Context.Attach(entity);

        public async Task<IDbContextTransaction> BeginTransaction ()
            => await _Context.Database.BeginTransactionAsync();

    }
}
