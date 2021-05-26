using System.Collections.Generic;

namespace WebAPI.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the number of meter readings that were successfully added to the database</returns>
        public int AddMeterReadingsIfCsvIsValid(IEnumerable<string> csvFileLines);
    }
}
