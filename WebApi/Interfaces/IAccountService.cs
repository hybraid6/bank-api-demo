using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(string userId, decimal initialDeposit);
        Task<Account> DepositAsync(Guid accountId, decimal amount);
        Task<Account> WithdrawAsync(Guid accountId, decimal amount);
    }
}
