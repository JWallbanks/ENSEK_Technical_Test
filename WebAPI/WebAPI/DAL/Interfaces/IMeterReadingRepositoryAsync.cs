using System;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IMeterReadingRepositoryAsync : IRepositoryAsync<MeterReading>
    {
        bool DoesMeterReadingExistInDb(int accountId, DateTime meterReadingDate);
    }
}