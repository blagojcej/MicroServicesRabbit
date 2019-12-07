using MicroServicesRabbit.Banking.App.Interfaces;
using MicroServicesRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MicroServicesRabbit.Banking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET api/banking
        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(_accountService.GetAccounts());
        }
    }
}