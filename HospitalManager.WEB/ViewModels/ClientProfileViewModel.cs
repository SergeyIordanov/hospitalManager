using System.Collections.Generic;
using HospitalManager.Core.Enums;

namespace HospitalManager.WEB.ViewModels
{
    public class ClientProfileViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public List<string> Roles { get; set; }
    }
}