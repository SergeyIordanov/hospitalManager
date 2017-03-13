using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.Core.Enums;
using HospitalManager.WEB.Infrastructure.Identity;
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
            //await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                var userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password
                };

                var claim = await UserService.SignInAsync(userDto);
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
            //await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    var userDto = new UserDto
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Address = model.Address,
                        Name = model.Name,
                        Gender = model.Gender,
                        Age = model.Age,
                        Role = "patient"
                    };

                    await UserService.RegisterAsync(userDto);

                    var claim = await UserService.SignInAsync(userDto);
                    SignIn(claim);

                    return RedirectToAction("Index", "Home");
                }
                catch (AuthException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

        private void SignIn(ClaimsIdentity claim)
        {
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true}, claim);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var loginCallbackurl = Url.Action(
                "ExternalLoginCallback",
                "Account", 
                new
                {
                    ReturnUrl = returnUrl
                });

            return new ChallengeResult(provider, loginCallbackurl);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            var claim = await UserService.ExternalSignInAsync(loginInfo.Login);

            if (claim != null)
            {
                SignIn(claim);

                return RedirectToAction("Index", "Home");
            }

            var externalRegisterViewModel = new ExternalLoginConfirmationViewModel { Email = loginInfo.Email };

            return View("ExternalLoginConfirmation", externalRegisterViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model)
        {
            //await SetInitialDataAsync();

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var userDto = new UserDto
                {
                    Name = model.Email,
                    Email = model.Email,
                    Address = model.Address,
                    Gender = model.Gender,
                    Age = model.Age,
                    Role = "patient"
                };

                try
                {
                    await UserService.ExternalRegisterAsync(userDto, info.Login);

                    var claim = await UserService.ExternalSignInAsync(info.Login);
                    SignIn(claim);

                    return RedirectToAction("Index", "Home");
                }
                catch (AuthException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
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
                Name = "Admin",
                Address = "Naukova ave., 2",
                Gender = Gender.Male,
                Age = 20,
                Role = "doctor"
            };

            var roles = new List<string> {"patient", "doctor"};

            await UserService.SetInitialDataAsync(admin, roles);
        }
    }
}