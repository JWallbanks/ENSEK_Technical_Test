using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public IActionResult meterReadingUploads(IEnumerable<string> csvFileLines)
        {

            var numOfSuccessfulReadings = _accountService.AddMeterReadingsIfCsvIsValid(csvFileLines);
            return Ok(numOfSuccessfulReadings);

        }
    }
}
