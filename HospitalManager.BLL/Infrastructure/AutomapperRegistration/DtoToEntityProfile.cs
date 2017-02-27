using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.DAL.Entities;

namespace HospitalManager.BLL.Infrastructure.AutomapperRegistration
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {

			CreateMap<TempDto, Temp>();

            CreateMap<ExampleDto, Example>();
        }
    }
}
