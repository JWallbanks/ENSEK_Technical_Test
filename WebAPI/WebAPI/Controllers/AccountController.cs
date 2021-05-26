using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost]
        [Route("meter-reading-uploads")]
        public async Task<IActionResult> meterReadingUploads(IEnumerable<string> csvFileLines)
        {
            var numOfSuccessfulReadings = await _accountService.AddMeterReadingsToDbIfCsvIsValidAsync(csvFileLines);
            return Ok(numOfSuccessfulReadings);

        }
    }
}
