using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Services
{
    public class MeterReadingApiService : IMeterReadingApiService
    {
        private readonly HttpClient _httpClient;

        public MeterReadingApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> AddMeterReadingsToDbFromCsvLinesAsync(IEnumerable<string> csvLines)
        {
            throw new NotImplementedException();
        }
    }
}
