using System.IO;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.WEB.ViewModels;

namespace HospitalManager.WEB.AutomapperRegistrations
{
    public class ViewModelToDtoProfile : Profile
    {
        public ViewModelToDtoProfile()
        {
            CreateMap<ClientProfileViewModel, ClientProfileDto>();
            CreateMap<PaymentViewModel, PaymentDto>();
            CreateMap<ArtifactCreateViewModel, ArtifactDto>()
                .ForMember(dto => dto.Content, expression => expression.ResolveUsing(vm =>
                {
                    using (var ms = new MemoryStream())
                    {
                        vm.Content.InputStream.CopyTo(ms);
                        return ms.GetBuffer();
                    }
                }));
        }
    }
}