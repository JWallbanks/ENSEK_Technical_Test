using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class AccountRepositoryAsync : RepositoryAsync<Account>, IAccountRepositoryAsync
    {
        private readonly EnsekContext _dbContext;

        public AccountRepositoryAsync(EnsekContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> GetAccountWithMeterReadings(int accountId)
        {
            return await _dbContext.Accounts
                .Include(a => a.MeterReadings)
                .SingleAsync(a => a.AccountId == accountId);

        }
    }
}
