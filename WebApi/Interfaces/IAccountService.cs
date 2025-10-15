using WebApi.Common;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<dynamic>> CreateAccountAsync(CreateAccountDto payload);
        
    }
}
