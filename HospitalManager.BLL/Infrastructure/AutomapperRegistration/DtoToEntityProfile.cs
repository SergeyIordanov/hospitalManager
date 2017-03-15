using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Esign;
using HospitalManager.Core.Encryption;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.BLL.Infrastructure.AutomapperRegistration
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<ClientProfileDto, ClientProfile>();
            CreateMap<PaymentDto, Payment>();
            CreateMap<ArtifactDto, Artifact>()
                .ForMember(artifact => artifact.Content, expression => expression
                .MapFrom(dto => dto.Content.ProtectBytes(Entropy.EntropyBytes)));
        }
    }
}