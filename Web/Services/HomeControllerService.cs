using System.Collections.Generic;
using System.Threading.Tasks;
using Web.DTOs;

namespace Web.Services
{
    public class HomeControllerService : IHomeControllerService
    {
        private readonly IMeterReadingApiService _meterReadingApiService;
        private readonly IAccountApiService _accountApiService;

        public HomeControllerService(IMeterReadingApiService meterReadingApiService, IAccountApiService accountApiService)
        {
            _meterReadingApiService = meterReadingApiService;
            _accountApiService = accountApiService;
        }

        public async Task<int> AddMeterReadingsToDbFromCsvLinesAsync(IEnumerable<string> csvLines)
        {
            var successfulReadings = await _meterReadingApiService.AddMeterReadingsToDbFromCsvLinesAsync(csvLines);
            return successfulReadings;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var accountDtos = await _accountApiService.GetAllAccountDtosAsync();
            return accountDtos;
        }

    }
}
