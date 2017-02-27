using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalManager.WEBMain.Startup))]
namespace HospitalManager.WEBMain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
