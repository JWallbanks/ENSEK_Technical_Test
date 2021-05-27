using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Services
{
    public class HomeControllerService : IHomeControllerService
    {
        private readonly IMeterReadingApiService _meterReadingApiService;

        public HomeControllerService(IMeterReadingApiService meterReadingApiService)
        {
            _meterReadingApiService = meterReadingApiService;
        }

        public async Task<int> AddMeterReadingsToDbFromCsvLinesAsync(IEnumerable<string> csvLines)
        {
            var successfulReadings = await _meterReadingApiService.AddMeterReadingsToDbFromCsvLinesAsync(csvLines);
            return successfulReadings;
        }

    }
}
