using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalManager.Core.Enums;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.DAL.Entities
{
    [Table("Payments")]
    public class Payment : BaseType
    {
        [Required]
        public string Signature { get; set; }

        public decimal Sum { get; set; }

        public string Currency { get; set; }

        public string Details { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        [Required]
        public virtual ClientProfile ClientProfile { get; set; }
    }
}