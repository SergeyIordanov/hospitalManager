using System.ComponentModel.DataAnnotations;
using System.Web;
using HospitalManager.WEB.Attributes;

namespace HospitalManager.WEB.ViewModels
{
    public class ArtifactCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "Choose a file .JPG, .JPEG, .PNG, .DOCX, .TXT")]
        [ValidFileTypeValidator]
        public HttpPostedFileBase Content { get; set; }
    }
}