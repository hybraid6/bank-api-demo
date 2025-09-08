using WebApi.Context;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _db;
        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public AccountService(AppDbContext db) => _db = db;

        public async Task<Account> CreateAccountAsync(string userId, decimal initialDeposit)
        {
            var account = new Account { Id = Guid.NewGuid(), UserId = userId, Balance = initialDeposit };
            _db.Accounts.Add(account);
            await _db.SaveChangesAsync();
            return account;
        }

        public async Task<Account> DepositAsync(Guid accountId, decimal amount)
        {
            await _lock.WaitAsync();
            try
            {
                var account = await _db.Accounts.FindAsync(accountId);
                if (account == null) throw new Exception("Account not found");
                account.Balance += amount;
                await _db.SaveChangesAsync();
                return account;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<Account> WithdrawAsync(Guid accountId, decimal amount)
        {
            await _lock.WaitAsync();
            try
            {
                var account = await _db.Accounts.FindAsync(accountId);
                if (account == null) throw new Exception("Account not found");
                if (account.Balance < amount) throw new Exception("Insufficient funds");
                account.Balance -= amount;
                await _db.SaveChangesAsync();
                return account;
            }
            finally
            {
                _lock.Release();
            }
        }
    }

}
