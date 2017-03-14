using System.Linq;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Esign;
using HospitalManager.Core.Encryption;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.BLL.Infrastructure.AutomapperRegistration
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<ClientProfile, ClientProfileDto>()
                .ForMember(dto => dto.Email, expression => expression.MapFrom(profile => profile.ApplicationUser.Email))
                .ForMember(dto => dto.Roles, expression => expression
                    .MapFrom(profile => profile.ApplicationUser.Roles.Select(x =>x.RoleId)));
            CreateMap<Payment, PaymentDto>();
            CreateMap<Artifact, ArtifactDto>()
                .ForMember(dto => dto.Content, expression => expression.MapFrom(artifact => artifact.Content.UnProtectBytes(Entropy.EntropyBytes)));
        }
    }
}