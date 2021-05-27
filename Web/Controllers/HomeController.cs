using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Web.DTOs;
using Web.Services;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IHomeControllerService _service;

        public HomeController(IHomeControllerService service)
        {
            _service = service;
        }

        [Route("~/")]
        [Route("~/[controller]/[action]")]
        public IActionResult Upload()
        {
            return View();
        }


        [Route("~/Ajax_UploadCsvFile")]
        [HttpPost]
        public async Task<IActionResult> Ajax_UploadCsvFile(IFormCollection data)
        {
            IFormFile file = data.Files["file"];
            var csvLines = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    csvLines.Add(line);
                }
            }

            var successfulReadings = await _service.AddMeterReadingsToDbFromCsvLinesAsync(csvLines);
            return Json(successfulReadings);
        }



        public IActionResult Index()
        {
            var vm = new List<AccountDto>
            {
                new AccountDto{FirstName = "John", LastName = "Smith"}
            };

            return View(vm);
        }

        [Route("{accountId}")]
        public IActionResult Details(int accountId)
        {
            return View();
        }

    }
}
