using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _accountService.CreateAccountAsync(payload);

            return StatusCode(serviceResponse.StatusCode, serviceResponse);
        }

    }
}
