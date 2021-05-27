using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IMeterReadingApiService
    {
        Task<int> AddMeterReadingsToDbFromCsvLinesAsync(IEnumerable<string> csvLines);
    }
}