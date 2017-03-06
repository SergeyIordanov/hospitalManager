using HospitalManager.Core.Enums;

namespace HospitalManager.BLL.DTO
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public string Signature { get; set; }

        public decimal Sum { get; set; }

        public string Currency { get; set; }

        public string Details { get; set; }

        public PaymentStatus Status { get; set; }

        public ClientProfileDto ClientProfile { get; set; }
    }
}