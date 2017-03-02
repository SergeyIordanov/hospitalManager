﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.DAL.Entities.Identity;
using HospitalManager.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace HospitalManager.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;

        public UserService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task ExternalRegisterAsync(UserDto userDto, UserLoginInfo info)
        {
            await CheckIfUserExists(userDto);

            var user = await CreateApplicationUser(userDto);

            await _database.UserManager.AddLoginAsync(user.Id, info);
            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);

            await CreateClientProfile(userDto, user);
        }

        public async Task RegisterAsync(UserDto userDto)
        {
            await CheckIfUserExists(userDto);

            var user = await CreateApplicationUser(userDto);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);

            await CreateClientProfile(userDto, user);
        }

        public async Task<ClaimsIdentity> SignInAsync(UserDto userDto)
        {
            ClaimsIdentity claim = null;

            var user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim = await _database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public async Task<ClaimsIdentity> ExternalSignInAsync(UserLoginInfo loginInfo)
        {
            ClaimsIdentity claim = null;

            var user = await _database.UserManager.FindAsync(loginInfo);

            if (user != null)
            {
                claim = await _database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public async Task SetInitialDataAsync(UserDto adminDto, IEnumerable<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await _database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole
                    {
                        Name = roleName
                    };

                    await _database.RoleManager.CreateAsync(role);
                }
            }

            await RegisterAsync(adminDto);
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        private async Task CreateClientProfile(UserDto userDto, ApplicationUser user)
        {
            var clientProfile = new ClientProfile
            {
                Id = user.Id,
                Address = userDto.Address,
                Name = userDto.Name
            };

            _database.ClientManager.Create(clientProfile);
            await _database.SaveAsync();
        }

        private async Task<ApplicationUser> CreateApplicationUser(UserDto userDto)
        {
            var user = new ApplicationUser
            {
                Email = userDto.Email,
                UserName = userDto.Email
            };

            IdentityResult result;

            if (userDto.Password != null)
            {
                result = await _database.UserManager.CreateAsync(user, userDto.Password);
            }
            else
            {
                result = await _database.UserManager.CreateAsync(user);
            }

            if (result.Errors.Any())
            {
                throw new AuthException(string.Empty, result.Errors.FirstOrDefault());
            }

            return user;
        }

        private async Task CheckIfUserExists(UserDto userDto)
        {
            var user = await _database.UserManager.FindByEmailAsync(userDto.Email);

            if (user != null)
            {
                throw new AuthException("Email", "User with such login already exists");
            }
        }
    }
}
