using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalManager.BLL.DTO;
using Microsoft.AspNet.Identity;

namespace HospitalManager.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task RegisterAsync(UserDto userDto);

        Task ExternalRegisterAsync(UserDto userDto, UserLoginInfo info);

        Task<ClaimsIdentity> SignInAsync(UserDto userDto);

        Task<ClaimsIdentity> ExternalSignInAsync(UserLoginInfo loginInfo);

        Task SetInitialDataAsync(UserDto adminDto, IEnumerable<string> roles);

        ClientProfileDto GetClientProfile(string userId);

        Task ChangeUserRole(string userId, string role);

        IEnumerable<ClientProfileDto> GetAllClientProfiles();
    }
}