using System.Collections.Generic;
using HospitalManager.BLL.DTO;

namespace HospitalManager.BLL.Interfaces
{
    public interface IArtifactService
    {
        void Create(ArtifactDto artifactDto, string userId);

        void Delete(int id);

        IEnumerable<ArtifactDto> GetUserArtifacts(string id);

        ArtifactDto GetArtifact(int id);
    }
}