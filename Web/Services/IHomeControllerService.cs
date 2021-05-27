using System.Collections.Generic;
using System.Threading.Tasks;
using Web.DTOs;

namespace Web.Services
{
    public interface IHomeControllerService
    {
        Task<int> AddMeterReadingsToDbFromCsvLinesAsync(IEnumerable<string> csvLines);
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
    }
}