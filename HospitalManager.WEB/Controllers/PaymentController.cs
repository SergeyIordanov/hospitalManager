﻿using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.Core.Enums;
using HospitalManager.WEB.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

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

        [HttpPost]
        public ActionResult Pay(PaymentResultViewModel paymentResult)
        {
            PaymentDto existingPayment;

            try
            {
                existingPayment = _paymentService
                    .GetBySignature(paymentResult?.Signature);
            }
            catch (EntityException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (paymentResult != null)
            {
                existingPayment.Sum = paymentResult.Payment.Sum;
                existingPayment.Currency = paymentResult.Payment.Currency;
                existingPayment.Details = paymentResult.Payment.Details;
                existingPayment.Status = PaymentStatus.Confirmed;

                _paymentService.Update(existingPayment);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}