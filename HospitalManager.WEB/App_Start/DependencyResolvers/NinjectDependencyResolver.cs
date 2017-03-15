using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using NLog;
using HospitalManager.BLL.Interfaces;
using HospitalManager.BLL.Services;

namespace HospitalManager.WEB.DependencyResolvers
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("HospitalDatabase");
            _kernel.Bind<IPaymentService>().To<PaymentService>();
            _kernel.Bind<IArtifactService>().To<ArtifactService>();

            _kernel.Bind<ILogger>().ToMethod(p =>
            {
                if (p.Request.Target?.Member.DeclaringType != null)
                {
                    return LogManager.GetLogger(p.Request.Target.Member.DeclaringType.ToString());
                }

                return LogManager.GetLogger("Filter logging");
            });
        }
    }
}