using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.WEB.ViewModels;
using Microsoft.AspNet.Identity;

namespace HospitalManager.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly IArtifactService _artifactService;

        public UserController(IPaymentService paymentService, IUserService userService, IArtifactService artifactService)
        {
            _paymentService = paymentService;
            _userService = userService;
            _artifactService = artifactService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult UserPage()
        {
            var userId = User.Identity.GetUserId();

            ClientProfileDto clientProfileDto;
            try
            {
                clientProfileDto = _userService.GetClientProfile(userId);
            }
            catch (EntityException ex)
            {
                return View("Error", model: ex.Message);
            }

            var clientProfileViewModel = Mapper.Map<ClientProfileViewModel>(clientProfileDto);
            var paymentDtos = _paymentService.Get(userId, false);
            var paymentViewModels = Mapper.Map<IEnumerable<PaymentViewModel>>(paymentDtos);
            var artifactDtos = _artifactService.GetUserArtifacts(userId);
            var artifactViewModels = Mapper.Map<IEnumerable<ArtifactDisplayViewModel>>(artifactDtos);

            var userPageModel = new UserPageViewModel
            {
                ClientProfile = clientProfileViewModel,
                Payments = paymentViewModels,
                Artifacts = artifactViewModels
            };

            return View(userPageModel);
        }

        [HttpGet]
        [Authorize(Roles = "doctor")]
        public async Task<ActionResult> ChangeRole(string userId, string role)
        {
            await _userService.ChangeUserRole(userId, role);

            return RedirectToAction("AdminPage");
        }

        [HttpGet]
        [Authorize(Roles = "doctor")]
        public ActionResult AdminPage()
        {
            var clientProfileDtos = _userService.GetAllClientProfiles();
            var clientProfileViewModels = Mapper.Map<List<ClientProfileViewModel>>(clientProfileDtos);

            return View(clientProfileViewModels);
        }
    }
}