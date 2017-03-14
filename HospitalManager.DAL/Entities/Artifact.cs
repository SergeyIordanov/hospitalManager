using System.ComponentModel.DataAnnotations;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.DAL.Entities
{
    public class Artifact : BaseType
    {
        public byte[] Content { get; set; }

        [Required]
        public virtual ClientProfile ClientProfile { get; set; }
    }
}