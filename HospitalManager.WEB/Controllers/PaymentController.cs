using System;
using System.Web.Mvc;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Interfaces;
using HospitalManager.Core.Enums;
using Microsoft.AspNet.Identity;

namespace HospitalManager.WEB.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public PaymentController(IPaymentService paymentService, IUserService userService)
        {
            _paymentService = paymentService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Pay()
        {
            var clientProfile = _userService.GetClientProfile(User.Identity.GetUserId());

            var payment = new PaymentDto
            {
                Signature = Guid.NewGuid().ToString(),
                Status = PaymentStatus.Initialized,
                ClientProfile = clientProfile
            };

            _paymentService.Create(payment);

            return View(model: payment.Signature);
        }
    }
}