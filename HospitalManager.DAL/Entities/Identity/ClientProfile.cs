using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalManager.Core.Enums;

namespace HospitalManager.DAL.Entities.Identity
{
    [Table("ClientProfiles")]
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Artifact> Artifacts { get; set; }
    }
}