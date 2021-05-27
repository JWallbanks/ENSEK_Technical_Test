using System.Collections.Generic;
using System.Threading.Tasks;
using Web.DTOs;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsDtosAsync();
        Task<AccountWithMeterReadingsDto> GetAccountWithMeterReadingsDtoAsync(int accountId);
    }
}