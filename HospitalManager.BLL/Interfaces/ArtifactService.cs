using System.Collections.Generic;
using HospitalManager.BLL.DTO;

namespace HospitalManager.BLL.Interfaces
{
    public interface IIllnessHistoryService
    {
        void Create(TreatmentArtifactDto artifactDto);

        void Update(TreatmentArtifactDto artifactDto);

        void Delete(int id);

        IEnumerable<TreatmentArtifactDto> GetUserIllnessHistory(string id);

        TreatmentArtifactDto GetIllnessHistoryArtifact(int id);
    }
}