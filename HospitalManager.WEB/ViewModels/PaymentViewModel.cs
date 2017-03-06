using HospitalManager.Core.Enums;

namespace HospitalManager.WEB.ViewModels
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        public string Signature { get; set; }

        public decimal Sum { get; set; }

        public string Currency { get; set; }

        public string Details { get; set; }

        public PaymentStatus Status { get; set; }

        public ClientProfileViewModel ClientProfile { get; set; }
    }
}