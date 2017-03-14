using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.DAL.Entities
{
    [Table("Artifacts")]
    public class Artifact : BaseType
    {
        [Required]
        public byte[] Content { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public virtual ClientProfile ClientProfile { get; set; }
    }
}