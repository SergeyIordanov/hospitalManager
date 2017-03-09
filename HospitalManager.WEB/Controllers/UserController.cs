using System.Collections.Generic;
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

        public UserController(IPaymentService paymentService, IUserService userService)
        {
            _paymentService = paymentService;
            _userService = userService;
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

            var userPageModel = new UserPageViewModel
            {
                ClientProfile = clientProfileViewModel,
                Payments = paymentViewModels
            };

            return View(userPageModel);
        }
    }
}