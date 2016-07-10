using System;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Yourslides.Model;
using Yourslides.Model.Api;
using Yourslides.Utils.DateTimeUtils;
using YourSlides.Web.Tools;

namespace YourSlides.Web.Mappings {
    public class DomainToApiMappingProfile : Profile {
        protected override void Configure() {
            CreateMap<Presentation, PresentationApi>()
                .ForMember(pa => pa.Id, map => map.MapFrom(p => Obfuscator.Obfuscate(p.Id)))
                .ForMember(pa => pa.Created, map => map.MapFrom(p => UnixTime.ToUnix(p.CreatedDateTime)))
                .ForMember(pa => pa.CanEdit, map => map.MapFrom(p => p.OwnerId == GetCurrentUserId()))
                .ForMember(pa => pa.LogoUrl160, map => map.MapFrom(p => Url.SlideUrlOrDefault(p.Id, p.LogoSlideIndex, "preview_small")))
                .ForMember(pa => pa.LogoUrl320, map => map.MapFrom(p => Url.SlideUrlOrDefault(p.Id, p.LogoSlideIndex, "preview_big")))
                .ForMember(pa => pa.LogoUrl720p, map => map.MapFrom(p => Url.SlideUrlOrDefault(p.Id, p.LogoSlideIndex, "720")))
                .ForMember(pa => pa.OwnerName, map => map.MapFrom(p => p.Owner.UserName));
            CreateMap<Comment, CommentApi>()
                .ForMember(ca => ca.OwnerName, map => map.MapFrom(c => c.Owner.UserName))
                .ForMember(ca => ca.Created, map => map.MapFrom(c => UnixTime.ToUnix(c.CreatedDateTime)))
                .ForMember(ca => ca.CanEdit, map => map.MapFrom(c => c.OwnerId == GetCurrentUserId()));
        }

        private static string GetCurrentUserId() {
            return HttpContext.Current.User.Identity.GetUserId();
        }
    }
}