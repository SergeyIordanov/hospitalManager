using System;
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
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.UseFacebookAuthentication(appId: "823512164453406", appSecret: "5d14a2960f8533ad779621a1726507fb");
        }
    }
}