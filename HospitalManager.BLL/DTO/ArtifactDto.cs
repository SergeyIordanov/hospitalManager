namespace HospitalManager.BLL.DTO
{
    public class ArtifactDto
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string Description { get; set; }

        public ClientProfileDto ClientProfileDto { get; set; }
    }
}