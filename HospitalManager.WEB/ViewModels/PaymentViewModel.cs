using System;
using HospitalManager.Core.Enums;
using Newtonsoft.Json;

namespace HospitalManager.WEB.ViewModels
{
    [Serializable]
    public class PaymentViewModel
    {
        public int Id { get; set; }

        [JsonProperty("order")]
        public string Signature { get; set; }

        [JsonProperty("amt")]
        public decimal Sum { get; set; }

        [JsonProperty("ccy")]
        public string Currency { get; set; }

        public string Details { get; set; }

        public PaymentStatus Status { get; set; }

        public ClientProfileViewModel ClientProfile { get; set; }
    }
}