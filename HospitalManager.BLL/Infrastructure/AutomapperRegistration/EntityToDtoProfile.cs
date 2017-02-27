using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.DAL.Entities;

namespace HospitalManager.BLL.Infrastructure.AutomapperRegistration
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {

			CreateMap<Temp, TempDto>();

            CreateMap<Example, ExampleDto>();        
        }
    }
}
