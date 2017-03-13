using AutoMapper;
using HospitalManager.BLL.Infrastructure.AutomapperRegistration;

namespace HospitalManager.WEB.AutomapperRegistrations
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ViewModelToDtoProfile());
                cfg.AddProfile(new DtoToViewModelProfile());

                cfg.AddProfile(new DtoToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });
        }
    }
}