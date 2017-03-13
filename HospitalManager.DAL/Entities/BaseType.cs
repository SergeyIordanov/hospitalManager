using System.ComponentModel.DataAnnotations;

namespace HospitalManager.DAL.Entities
{
    public class BaseType
    {
        [Key]
        public int Id { get; set; }
    }
}
