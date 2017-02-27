using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Infrastructure.Identity;

namespace HospitalManager.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task SetInitialData(UserDto adminDto, List<string> roles);
    }
}
