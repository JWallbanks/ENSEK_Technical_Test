using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> AddMeterReadingsToDbIfCsvIsValidAsync(IEnumerable<string> csvFileLines)
        {
            var csvLinesList = csvFileLines.ToList();
            var successfulReadings = 0;

            // i = 1 to skip the line with column headings
            for (var i = 1; i < csvLinesList.Count; i++)
            {
                var csvLine = csvLinesList[i];
                var isSuccessfulReading = await AddMeterReadingToDbIfCsvIsValidAsync(csvLine);
                if (isSuccessfulReading)
                    successfulReadings++;
            }
            await _uow.CommitAsync();

            return successfulReadings;
        }

        private async Task<bool> AddMeterReadingToDbIfCsvIsValidAsync(string csvLine)
        {
            try
            {
                string[] values = csvLine.Split(',');
                var accountId = Convert.ToInt32(values[0]);
                var account = await _uow.AccountRepositoryAsync.FindAsync(accountId);
                if (account == null)
                    return false;

                var meterReading = new MeterReading();
                meterReading.Account = account;
                meterReading.MeterReadingDate = Convert.ToDateTime(values[1]);
                meterReading.MeterReadValue = Convert.ToInt32(values[2]);

                if (_uow.MeterReadingRepositoryAsync.DoesMeterReadingExistInDb(accountId, meterReading.MeterReadingDate))
                    return false;

                if (!Regex.IsMatch(meterReading.MeterReadValue.ToString(), @"^\d{5}$"))
                    return false;

                await _uow.MeterReadingRepositoryAsync.InsertAsync(meterReading);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
