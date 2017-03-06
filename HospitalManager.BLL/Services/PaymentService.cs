using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.DAL.Entities;
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

        public IEnumerable<PaymentDto> Get()
        {
            var payments = _unitOfWork.Payments.GetAll().ToList();
            var paymentDtos = Mapper.Map<IEnumerable<PaymentDto>>(payments);

            return paymentDtos;
        }

        public IEnumerable<PaymentDto> Get(string clientProfileId)
        {
            var payments = _unitOfWork.Payments
                .Find(payment => payment.ClientProfile.Id.Equals(clientProfileId))
                .ToList();
            var paymentDtos = Mapper.Map<IEnumerable<PaymentDto>>(payments);

            return paymentDtos;
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
    }
}