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
    public class ArtifactService : IArtifactService
    {
        private readonly IUnitOfWork _uow;

        public ArtifactService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Create(ArtifactDto artifactDto, string userId)
        {
            var artifact = Mapper.Map<Artifact>(artifactDto);
            var user = _uow.ClientManager.Get(userId);
            artifact.ClientProfile = user;

            _uow.TreatmentArtifacts.Create(artifact);
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

            _uow.TreatmentArtifacts.Delete(id);
            _uow.Save();
        }

        public IEnumerable<ArtifactDto> GetUserArtifacts(string id)
        {
            var artifacts = _uow.TreatmentArtifacts.Find(x => x.ClientProfile.Id.Equals(id)).ToList();
            var artifactDtos = Mapper.Map<IEnumerable<ArtifactDto>>(artifacts);

            return artifactDtos;
        }

        public ArtifactDto GetArtifact(int id)
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