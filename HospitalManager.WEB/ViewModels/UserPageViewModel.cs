using System.Collections.Generic;

namespace HospitalManager.WEB.ViewModels
{
    public class UserPageViewModel
    {
        public IEnumerable<PaymentViewModel> Payments { get; set; }

        public ClientProfileViewModel ClientProfile { get; set; }
    }
}