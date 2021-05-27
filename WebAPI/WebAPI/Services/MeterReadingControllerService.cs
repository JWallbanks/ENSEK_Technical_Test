using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class MeterReadingControllerService : IMeterReadingControllerService
    {
        private readonly IUnitOfWork _uow;

        public MeterReadingControllerService(IUnitOfWork uow)
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
                var meterReadingDate = Convert.ToDateTime(values[1]);
                var meterReadValue = Convert.ToInt32(values[2]);
                var account = await _uow.AccountRepositoryAsync.FindAsync(accountId);

                if (!IsValuesValid(account, meterReadingDate, meterReadValue))
                    return false;

                MeterReading meterReading = CreateMeterReading(meterReadingDate, meterReadValue, account);

                await _uow.MeterReadingRepositoryAsync.InsertAsync(meterReading);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool IsValuesValid(Account account, DateTime meterReadingDate, int meterReadValue)
        {
            if (account == null)
                return false;

            if (_uow.MeterReadingRepositoryAsync.DoesMeterReadingExistInDb(account.AccountId, meterReadingDate))
                return false;

            if (!Regex.IsMatch(meterReadValue.ToString(), @"^\d{5}$"))
                return false;

            return true;
        }

        private MeterReading CreateMeterReading(DateTime meterReadingDate, int meterReadValue, Account account)
        {
            var meterReading = new MeterReading();
            meterReading.Account = account;
            meterReading.MeterReadingDate = meterReadingDate;
            meterReading.MeterReadValue = meterReadValue;
            return meterReading;
        }

    }
}
