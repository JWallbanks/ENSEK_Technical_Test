using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnsekContext _context;

        public IMeterReadingRepositoryAsync MeterReadingRepositoryAsync { get; }
        public IAccountRepositoryAsync AccountRepositoryAsync { get; }

        public UnitOfWork
            (
            EnsekContext context,
            IMeterReadingRepositoryAsync meterReadingRepositoryAsync,
            IAccountRepositoryAsync accountRepositoryAsync
            )
        {
            _context = context;
            MeterReadingRepositoryAsync = meterReadingRepositoryAsync;
            AccountRepositoryAsync = accountRepositoryAsync;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}