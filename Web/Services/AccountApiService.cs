using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.DTOs;

namespace Web.Services
{
    public class AccountApiService : IAccountApiService
    {
        private readonly HttpClient _httpClient;

        public AccountApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountDtosAsync()
        {
            string apiCall = "api/account/get-all-accounts";
            IEnumerable<AccountDto> accountDtos = await GetObjectFromApiCallAsync<IEnumerable<AccountDto>>(apiCall);
            return accountDtos;
        }

        public async Task<AccountWithMeterReadingsDto> GetAccountWithMeterReadingsDtoAsync(int accountId)
        {
            string apiCall = $"api/account/get-account-with-meter-readings/{accountId}";

            var accountWithMeterReadingsDto = await GetObjectFromApiCallAsync<AccountWithMeterReadingsDto>(apiCall);
            return accountWithMeterReadingsDto;
        }

        private async Task<T> GetObjectFromApiCallAsync<T>(string apiCall) where T : class
        {
            var response = await _httpClient.GetAsync(apiCall);
            var result = await response.Content.ReadAsStringAsync();

            var resultObject = JsonConvert.DeserializeObject<T>(result);
            return resultObject;
        }
    }
}
