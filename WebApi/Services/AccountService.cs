using Microsoft.AspNetCore.Identity;
using System.Net;
using WebApi.Common;
using WebApi.Context;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResponse<dynamic>> CreateAccountAsync(CreateAccountDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>(); 

            try
            {
                var isUserExist = await _userManager.FindByEmailAsync(payload.Email);

                if (isUserExist is not null)
                {
                    serviceResponse.Data = new { };
                    serviceResponse.Message = "User already exists";
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    return serviceResponse;
                }

                var newUser = new User
                {
                    FirstName = payload.FirstName,
                    LastName = payload.LastName,
                    Address = payload.Address,
                    Email = payload.Email,
                    PhoneNumber = payload.PhoneNumber,
                    UserName = payload.Email,
                    UpdatedAt = DateTime.UtcNow,
                };

                var response = await _userManager.CreateAsync(newUser, payload.Password);

                if (!response.Succeeded)
                {
                    var errorMsg = string.Empty;

                    foreach (var error in response.Errors)
                    {
                        errorMsg = error.Description;
                    }

                    serviceResponse.Data = new { };
                    serviceResponse.Message = errorMsg;
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    return serviceResponse;
                }

                serviceResponse.Data = payload;
                serviceResponse.StatusCode = (int)HttpStatusCode.OK;
                serviceResponse.Message = "Registration Successful";
                serviceResponse.Success = true;


            }
            catch(Exception ex)
            {
                serviceResponse.Data = new { };
                serviceResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                serviceResponse.Message = "Internal Server Error";
            }
            return serviceResponse;
        }
    }

}
