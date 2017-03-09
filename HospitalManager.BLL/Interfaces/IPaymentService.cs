using System.Collections.Generic;
using HospitalManager.BLL.DTO;

namespace HospitalManager.BLL.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<PaymentDto> Get(bool includeInitialized = true);

        IEnumerable<PaymentDto> Get(string clientProfileId, bool includeInitialized = true);

        PaymentDto GetBySignature(string signature);

        PaymentDto Get(int id);

        void Create(PaymentDto payment);

        void Update(PaymentDto payment);

        void Delete(int id);
    }
}