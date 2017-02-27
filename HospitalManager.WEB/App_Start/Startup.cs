using System.Web.Mvc;
using HospitalManager.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(HospitalManager.WEB.Startup))]

namespace HospitalManager.WEB
{
    public class Startup
    {
        [Inject]
        private IUserService UserService => DependencyResolver.Current.GetService<IUserService>();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(()=>UserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}