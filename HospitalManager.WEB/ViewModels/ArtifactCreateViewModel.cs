using System.Web;

namespace HospitalManager.WEB.ViewModels
{
    public class ArtifactCreateViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase Content { get; set; }
    }
}