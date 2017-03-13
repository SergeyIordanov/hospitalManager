using System.ComponentModel.DataAnnotations;
using HospitalManager.Core.Enums;

namespace HospitalManager.WEB.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}