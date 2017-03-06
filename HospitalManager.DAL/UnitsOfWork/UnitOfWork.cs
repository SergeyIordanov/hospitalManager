using System;
using System.Threading.Tasks;
using HospitalManager.DAL.EF;
using HospitalManager.DAL.Entities.Identity;
using HospitalManager.DAL.Identity;
using HospitalManager.DAL.Interfaces;
using HospitalManager.DAL.Repositories.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalManager.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        private ApplicationUserManager _userManager;

        private ApplicationRoleManager _roleManager;

        private IClientManager _clientManager;

        private bool _disposed;

        public UnitOfWork(string databaseConnectionString)
        {
            _databaseContext = new DatabaseContext(databaseConnectionString);

            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_databaseContext));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_databaseContext));
            _clientManager = new ClientManager(_databaseContext);
        }

        public ApplicationUserManager UserManager => _userManager ?? (_userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_databaseContext)));

        public IClientManager ClientManager => _clientManager ?? (_clientManager = new ClientManager(_databaseContext));

        public ApplicationRoleManager RoleManager => _roleManager ?? (_roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_databaseContext)));

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
        public void Save()
        {
            _databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _userManager.Dispose();
                _roleManager.Dispose();
                _clientManager.Dispose();
                _databaseContext.Dispose();
            }

            _disposed = true;
        }
    }
}
