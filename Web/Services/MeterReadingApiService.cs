using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            string apiCall = "api/meterReading/meter-reading-uploads";

            string json = JsonConvert.SerializeObject(csvLines);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiCall, data);
            var result = await response.Content.ReadAsStringAsync();
            var successfulReadings = Convert.ToInt32(result);

            return successfulReadings;
        }
    }
}
