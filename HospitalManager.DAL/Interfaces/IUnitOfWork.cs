using System;
using System.Threading.Tasks;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Identity;

namespace HospitalManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }

        IClientManager ClientManager { get; }

        ApplicationRoleManager RoleManager { get; }

        IRepository<Payment> Payments { get; }

        void Save();

        Task SaveAsync();
    }
}