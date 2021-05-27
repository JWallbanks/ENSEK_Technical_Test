using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : Controller
    {
        private readonly IMeterReadingControllerService _meterReadingService;

        public MeterReadingController(IMeterReadingControllerService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }


        [HttpPost]
        [Route("~/meter-reading-uploads")]
        public async Task<IActionResult> meterReadingUploads(IEnumerable<string> csvFileLines)
        {
            var numOfSuccessfulReadings = await _meterReadingService.AddMeterReadingsToDbIfCsvIsValidAsync(csvFileLines);
            return Ok(numOfSuccessfulReadings);

        }
    }
}
