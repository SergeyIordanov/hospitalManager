using AutoMapper;
using HospitalManager.BLL.DTO;
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
        }
    }
}