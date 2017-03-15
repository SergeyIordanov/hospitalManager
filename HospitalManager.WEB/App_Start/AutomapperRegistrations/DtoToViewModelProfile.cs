using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.WEB.ViewModels;

namespace HospitalManager.WEB.AutomapperRegistrations
{
    public class DtoToViewModelProfile : Profile
    {
        public DtoToViewModelProfile()
        {
            CreateMap<ClientProfileDto, ClientProfileViewModel>();
            CreateMap<PaymentDto, PaymentViewModel>();
            CreateMap<ArtifactDto, ArtifactCreateViewModel>();
            CreateMap<ArtifactDto, ArtifactDisplayViewModel>();
        }
    }
}