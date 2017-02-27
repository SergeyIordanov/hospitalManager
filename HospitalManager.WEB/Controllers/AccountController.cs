using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Interfaces;
using HospitalManager.WEB.ViewModels.Account;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace HospitalManager.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                var userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password
                };

                var claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties{ IsPersistent = true }, claim);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };

                var operationDetails = await UserService.Create(userDto);

                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            var admin = new UserDto
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                Password = "Admin_1234",
                Name = "Alex Admin",
                Address = "Naukova ave., 2",
                Role = "admin"
            };

            var roles = new List<string> {"user", "admin"};

            await UserService.SetInitialData(admin, roles);
        }
    }
}