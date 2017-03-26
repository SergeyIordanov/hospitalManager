namespace HospitalManager.BLL.DTO
{
    public class ArtifactDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Extension { get; set; }

        public string Path { get; set; }

        public ClientProfileDto ClientProfileDto { get; set; }
    }
}