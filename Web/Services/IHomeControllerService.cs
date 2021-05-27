using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IHomeControllerService
    {
        Task<int> AddMeterReadingsToDbFromCsvLinesAsync(IEnumerable<string> csvLines);
    }
}