using System.Collections.Generic;
using System.Threading.Tasks;
using Web.DTOs;

namespace Web.Services
{
    public interface IAccountApiService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountDtosAsync();
        Task<AccountWithMeterReadingsDto> GetAccountWithMeterReadingsDtoAsync(int accountId);
    }
}