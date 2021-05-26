using System;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class MeterReadingRepositoryAsync : RepositoryAsync<MeterReading>, IMeterReadingRepositoryAsync
    {
        private readonly EnsekContext _dbContext;

        public MeterReadingRepositoryAsync(EnsekContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool DoesMeterReadingExistInDb(int accountId, DateTime meterReadingDate)
        {
            return _dbContext.MeterReadings
                .Any(m => m.Account.AccountId == accountId && m.MeterReadingDate == meterReadingDate);
        }
    }
}
