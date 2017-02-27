using HospitalManager.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace HospitalManager.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
