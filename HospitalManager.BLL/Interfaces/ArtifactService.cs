using System.Collections.Generic;
using HospitalManager.BLL.DTO;

namespace HospitalManager.BLL.Interfaces
{
    public interface IIllnessHistoryService
    {
        void Create(ArtifactDto artifactDto);

        void Update(ArtifactDto artifactDto);

        void Delete(int id);

        IEnumerable<ArtifactDto> GetUserIllnessHistory(string id);

        ArtifactDto GetIllnessHistoryArtifact(int id);
    }
}