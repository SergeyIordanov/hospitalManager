using HospitalManager.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace HospitalManager.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {
            //var adminRole = new ApplicationRole
            //{
            //    Name = "admin"
            //};

            //var userRole = new ApplicationRole
            //{
            //    Name = "user"
            //};

            //store.CreateAsync(adminRole);
            //store.CreateAsync(userRole);
        }
    }
}
