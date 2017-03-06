using HospitalManager.DAL.EF;
using HospitalManager.DAL.Entities;
using Ninject.Modules;
using HospitalManager.DAL.Interfaces;
using HospitalManager.DAL.Repositories;
using HospitalManager.DAL.Repositories.Identity;
using HospitalManager.DAL.UnitsOfWork;

namespace HospitalManager.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<DatabaseContext>().To<DatabaseContext>().WithConstructorArgument(_connectionString);
            Bind<IRepository<Payment>>().To<CommonRepository<Payment>>();
            Bind<IClientManager>().To<ClientManager>();
        }
    }
}