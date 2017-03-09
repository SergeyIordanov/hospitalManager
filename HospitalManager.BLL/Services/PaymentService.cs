using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.Core.Enums;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Entities.Identity;
using HospitalManager.DAL.Interfaces;

namespace HospitalManager.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PaymentDto> Get(bool includeInitialized = true)
        {
            var paymentsQuery = _unitOfWork.Payments.GetAll();

            if (!includeInitialized)
            {
                paymentsQuery = paymentsQuery
                    .Where(payment => payment.Status != PaymentStatus.Initialized);
            }

            var paymentsList = paymentsQuery.ToList();
            var paymentDtos = Mapper.Map<IEnumerable<PaymentDto>>(paymentsList);

            return paymentDtos;
        }

        public IEnumerable<PaymentDto> Get(string clientProfileId, bool includeInitialized = true)
        {
            var paymentsQuery = _unitOfWork.Payments
                .Find(payment => payment.ClientProfile.Id.Equals(clientProfileId));

            if (!includeInitialized)
            {
                paymentsQuery = paymentsQuery
                    .Where(payment => payment.Status != PaymentStatus.Initialized);
            }

            var paymentsList = paymentsQuery.ToList();
            var paymentDtos = Mapper.Map<IEnumerable<PaymentDto>>(paymentsList);

            return paymentDtos;
        }

        public PaymentDto GetBySignature(string signature)
        {
            var payment = _unitOfWork.Payments
                .Find(p => p.Signature.Equals(signature))
                .FirstOrDefault();

            if (payment == null)
            {
                throw new EntityNotFoundException(
                    $"Cannot find payment with such signature. Signature: {signature}", 
                    "Payment");
            }

            var paymentDto = Mapper.Map<PaymentDto>(payment);

            return paymentDto;
        }

        public PaymentDto Get(int id)
        {
            var payment = _unitOfWork.Payments.Get(id);

            if (payment == null)
            {
                throw new EntityNotFoundException($"Payment with such id cannot be found. Id: {id}", "Payment");
            }

            var paymentDto = Mapper.Map<PaymentDto>(payment);

            return paymentDto;
        }

        public void Create(PaymentDto paymentDto)
        {
            var payment = Mapper.Map<Payment>(paymentDto);

            AssignClientProfile(payment);

            _unitOfWork.Payments.Create(payment);
            _unitOfWork.Save();
        }

        public void Update(PaymentDto paymentDto)
        {
            var existingPayment = _unitOfWork.Payments.Get(paymentDto.Id);

            if (existingPayment == null)
            {
                throw new EntityNotFoundException(
                    $"Payment with such id cannot be found for update. Id: {paymentDto.Id}", 
                    "Payment");
            }

            Mapper.Map(paymentDto, existingPayment);
            AssignClientProfile(existingPayment);

            _unitOfWork.Payments.Update(existingPayment);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var payment = _unitOfWork.Payments.Get(id);

            if (payment == null)
            {
                throw new EntityNotFoundException(
                    $"Payment with such id cannot be found for deleting. Id: {id}", 
                    "Payment");
            }

            _unitOfWork.Payments.Delete(id);
            _unitOfWork.Save();
        }

        private void AssignClientProfile(Payment payment)
        {
            if (payment.ClientProfile == null)
            {
                return;
            }

            ClientProfile clientProfile =
                _unitOfWork.ClientManager.Get(payment.ClientProfile.Id);

            if (clientProfile == null)
            {
                throw new EntityNotFoundException(
                    $"Cannot find client profile to create a new payment. Client profile id: {payment.ClientProfile.Id}",
                    "ClientProfile");
            }

            payment.ClientProfile = clientProfile;
        }
    }
}