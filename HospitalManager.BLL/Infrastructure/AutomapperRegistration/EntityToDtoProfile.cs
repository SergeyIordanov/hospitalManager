using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.BLL.Infrastructure.AutomapperRegistration
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<ClientProfile, ClientProfileDto>()
                .ForMember(dto => dto.Email, expression => expression.MapFrom(profile => profile.ApplicationUser.Email));
            CreateMap<Payment, PaymentDto>();
        }
    }
}