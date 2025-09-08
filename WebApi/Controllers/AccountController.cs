using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount(CreateAccountDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            var account = await _service.CreateAccountAsync(user.Id, dto.InitialDeposit);
            return Ok(account);
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(Guid id, decimal amount)
        {
            var account = await _service.DepositAsync(id, amount);
            return Ok(account);
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid id, decimal amount)
        {
            var account = await _service.WithdrawAsync(id, amount);
            return Ok(account);
        }
    }
}
