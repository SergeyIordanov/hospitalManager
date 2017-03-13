using System.Collections.Generic;
using HospitalManager.BLL.DTO;

namespace HospitalManager.BLL.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<PaymentDto> Get();

        IEnumerable<PaymentDto> Get(string clientProfileId);

        PaymentDto Get(int id);

        void Create(PaymentDto payment);

        void Update(PaymentDto payment);

        void Delete(int id);
    }
}