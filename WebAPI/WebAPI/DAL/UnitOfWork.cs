using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnsekContext _context;

        public IMeterReadingRepositoryAsync MeterReadingRepositoryAsync { get; }
        public IRepositoryAsync<Account> AccountRepositoryAsync { get; }

        public UnitOfWork
            (
            EnsekContext context,
            IMeterReadingRepositoryAsync meterReadingRepositoryAsync,
            IRepositoryAsync<Account> accountRepositoryAsync
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