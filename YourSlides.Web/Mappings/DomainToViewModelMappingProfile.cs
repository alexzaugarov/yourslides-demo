using AutoMapper;
using Yourslides.Model;
using YourSlides.Web.Models;
using YourSlides.Web.Tools;

namespace YourSlides.Web.Mappings {
    public class DomainToViewModelMappingProfile : Profile {
        protected override void Configure() {
            CreateMap<Presentation, PresentationViewModel>()
                .ForMember(pvm => pvm.OwnerName, map => map.MapFrom(p => p.Owner.UserName))
                .ForMember(pvm => pvm.Id, map => map.MapFrom(p => Obfuscator.Obfuscate(p.Id)));
        }
    }
}