namespace HospitalManager.WEB.ViewModels
{
    public class ArtifactViewModel
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public ClientProfileViewModel ClientProfile { get; set; }
    }
}