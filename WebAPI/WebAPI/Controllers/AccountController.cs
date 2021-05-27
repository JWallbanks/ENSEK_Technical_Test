using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpGet]
        [Route("get-all-accounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accountDtos = await _accountService.GetAllAccountsAsDtosAsync();
            return Ok(accountDtos);

        }
    }
}
