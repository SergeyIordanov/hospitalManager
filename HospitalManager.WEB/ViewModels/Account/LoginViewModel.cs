using System.ComponentModel.DataAnnotations;

namespace HospitalManager.WEB.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}