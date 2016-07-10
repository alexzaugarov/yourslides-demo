using AutoMapper;
using Yourslides.Model;
using Yourslides.Model.Api;
using Yourslides.Utils.DateTimeUtils;
using YourSlides.Web.Tools;

namespace YourSlides.Web.Mappings {
    public class ApiToDomainMappingProfile : Profile{
        protected override void Configure() {
            CreateMap<PresentationApi, Presentation>()
                .ForMember(p => p.Id, map => map.MapFrom(pa => Obfuscator.Deobfuscate(pa.Id)))
                .ForMember(p => p.CreatedDateTime, map => map.MapFrom(pa => UnixTime.ToDateTime(pa.Created)))
                .ForMember(p => p.OwnerId, map => map.Ignore());
            CreateMap<CommentApi, Comment>()
                .ForMember(c => c.OwnerId, map => map.Ignore());
        }
    }
}