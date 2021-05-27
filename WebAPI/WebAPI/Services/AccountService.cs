using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DAL;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsDtosAsync()
        {
            var accounts = await _uow.AccountRepositoryAsync.GetEntireListAsync();

            var accountDtos = accounts
                .Select(a => new AccountDto
                { AccountId = a.AccountId, FirstName = a.FirstName, LastName = a.LastName }
                ).ToList();

            return accountDtos;
        }
    }
}
