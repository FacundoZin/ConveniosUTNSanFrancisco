using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using APIconvenios.Repositorio;

namespace APIconvenios.Common
{
    public class TransactionManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IConvenioEspecificoRepository _convenioEspecificoRepository;
        private readonly IInvolucradosRepository _involucradosRepository;

        public TransactionManager(ApplicationDbContext context, IConvenioEspecificoRepository convenioEspecificoRepository,
            IInvolucradosRepository involucradosrepository) 
        {
            _context = context;
            _convenioEspecificoRepository = convenioEspecificoRepository;   
            _involucradosRepository = involucradosrepository;
        }

        public async Task CreateConvenioEspecificoTransaction (ConvenioEspecifico convenio, List<Involucrados> involucrados)
        {
           var Transaction = await _context.Database.BeginTransactionAsync();

           
        }
       
    }
}
