using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IMeterReadingControllerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the number of meter readings that were successfully added to the database</returns>
        public Task<int> AddMeterReadingsToDbIfCsvIsValidAsync(IEnumerable<string> csvFileLines);
    }
}
