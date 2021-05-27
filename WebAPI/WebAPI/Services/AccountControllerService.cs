using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.DTOs;
using WebAPI.DAL;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public class AccountControllerService : IAccountControllerService
    {
        private readonly IUnitOfWork _uow;

        public AccountControllerService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsDtosAsync()
        {
            var accounts = await _uow.AccountRepositoryAsync.GetEntireListAsync();

            var accountDtos = accounts.Select(a => new AccountDto
            { AccountId = a.AccountId, FirstName = a.FirstName, LastName = a.LastName }
                ).ToList();

            return accountDtos;
        }

        public async Task<AccountWithMeterReadingsDto> GetAccountWithMeterReadingsDtoAsync(int accountId)
        {
            var accountWithMeterReadings = await _uow.AccountRepositoryAsync.GetAccountWithMeterReadings(accountId);

            var accountDto = new AccountDto
            {
                AccountId = accountWithMeterReadings.AccountId,
                FirstName = accountWithMeterReadings.FirstName,
                LastName = accountWithMeterReadings.LastName
            };

            var accountWithMeterReadingsDto = new AccountWithMeterReadingsDto
            {
                Account = accountDto,
                MeterReadings = accountWithMeterReadings.MeterReadings
                .Select(m => new MeterReadingDto
                {
                    MeterReadingId = m.MeterReadingId,
                    MeterReadingDate = m.MeterReadingDate,
                    MeterReadingValue = m.MeterReadValue
                }
                ).ToList()
            };

            return accountWithMeterReadingsDto;
        }

    }
}
