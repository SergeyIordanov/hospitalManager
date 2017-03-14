using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Interfaces;

namespace HospitalManager.BLL.Services
{
    public class ArtifactService : IIllnessHistoryService
    {
        private readonly IUnitOfWork _uow;

        public ArtifactService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Create(ArtifactDto artifactDto)
        {
            var artifact = Mapper.Map<Artifact>(artifactDto);

            _uow.TreatmentArtifacts.Create(artifact);
            _uow.Save();
        }

        public void Update(ArtifactDto artifactDto)
        {
            var artifact = _uow.TreatmentArtifacts.Get(artifactDto.Id);

            if (artifact == null)
            {
                throw new EntityNotFoundException(
                    $"Payment with such id cannot be found for update. Id: {artifact.Id}",
                    "Payment");
            }

            Mapper.Map(artifactDto, artifact);
            _uow.TreatmentArtifacts.Update(artifact);
            _uow.Save();
        }

        public void Delete(int id)
        {
            var artifact = _uow.TreatmentArtifacts.Get(id);

            if (artifact == null)
            {
                throw new EntityNotFoundException(
                    $"Artifact with such id cannot be found for deleting. Id: {id}",
                    "TreatmentArtifact");
            }

            _uow.Payments.Delete(id);
            _uow.Save();
        }

        public IEnumerable<ArtifactDto> GetUserIllnessHistory(string id)
        {
            var artifacts = _uow.TreatmentArtifacts.Find(x => x.ClientProfile.Id.Equals(id)).ToList();
            var artifactDtos = Mapper.Map<IEnumerable<ArtifactDto>>(artifacts);

            return artifactDtos;
        }

        public ArtifactDto GetIllnessHistoryArtifact(int id)
        {
            var artifact = _uow.TreatmentArtifacts.Get(id);

            if (artifact == null)
            {
                throw new EntityNotFoundException(
                    $"Artifact with such id cannot be found for read. Id: {id}",
                    "TreatmentArtifact");
            }

            var artifactDto = Mapper.Map<ArtifactDto>(artifact);

            return artifactDto;
        }
    }
}