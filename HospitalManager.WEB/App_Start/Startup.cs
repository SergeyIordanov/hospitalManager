using System;
using System.Web.Mvc;
using HospitalManager.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
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

            var x = new FacebookAuthenticationOptions();
            x.Scope.Add("email");
            x.Scope.Add("public_profile");
            x.Scope.Add("user_birthday");
            x.AppId = "823512164453406";
            x.AppSecret = "5d14a2960f8533ad779621a1726507fb";

            x.Provider = new FacebookAuthenticationProvider
            {
                OnAuthenticated = async context =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                }
            };

            x.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseFacebookAuthentication(x);

            //app.UseFacebookAuthentication(appId: "823512164453406", appSecret: "5d14a2960f8533ad779621a1726507fb");
        }
    }
}