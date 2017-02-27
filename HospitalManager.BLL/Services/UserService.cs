using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Infrastructure.Identity;
using HospitalManager.BLL.Interfaces;
using HospitalManager.DAL.Entities.Identity;
using HospitalManager.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace HospitalManager.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDto userDto)
        {
            var user = await Database.UserManager.FindByEmailAsync(userDto.Email);

            if (user != null)
            {
                return new OperationDetails(false, "User with such login already exists", "Email");
            }

            user = new ApplicationUser
            {
                Email = userDto.Email,
                UserName = userDto.Email
            };

            var result = await Database.UserManager.CreateAsync(user, userDto.Password);

            if (result.Errors.Any())
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault(), string.Empty);
            }

            await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);

            var clientProfile = new ClientProfile
            {
                Id = user.Id,
                Address = userDto.Address,
                Name = userDto.Name
            };

            Database.ClientManager.Create(clientProfile);
            await Database.SaveAsync();

            return new OperationDetails(true, "Succesfull registration", string.Empty);
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            ClaimsIdentity claim = null;
            var user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public async Task SetInitialData(UserDto adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole
                    {
                        Name = roleName
                    };

                    await Database.RoleManager.CreateAsync(role);
                }
            }

            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
