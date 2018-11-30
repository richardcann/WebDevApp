using AutoMapper;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Models.InternalDTO;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Property, BasicProperty>();
            CreateMap<AppUser, BasicAppUser>();
            CreateMap<Image, BasicImage>();
            CreateMap<Rejection, BasicRejection>();

            CreateMap<BPBasicImages, BPImages>()
                .ForMember(f => f.Images, o => o.ResolveUsing(b =>
                {
                    if(b.Images == null)
                    {
                        return null;
                    }
                    return ResolveImages(b.Images);
                }
            ));

            CreateMap<BPBIRejections, BPIRejections>()
                .ForMember(f => f.Images, o => o.ResolveUsing(b =>
                {
                    if (b.Images == null)
                    {
                        return null;
                    }
                    return ResolveImages(b.Images);
                }
                ))
                .ForMember(f => f.Rejections, o => o.ResolveUsing(b =>
                {
                    if(b.Rejections == null)
                    {
                        return null;
                    }
                    return b.Rejections.OrderByDescending(x => x.Timestamp).ToList();
                }
                ));

            CreateMap<AppUser, LoginSuccess>();
            
            CreateMap<AddProperty, Property>()
                .ForMember(a => a.Images, opt => opt.Ignore());
            CreateMap<BasicImage, Image>();
            CreateMap<BasicRejection, Rejection>();

            CreateMap<BPImages, Property>()
                 .ForMember(p => p.AppUserRef, o => o.ResolveUsing(b => b.Landlord.Username));
            CreateMap<BPIRejections, Property>()
                 .ForMember(p => p.AppUserRef, o => o.ResolveUsing(b => b.Landlord.Username));

        }

        private static List<string> ResolveImages(List<BasicImage> basicImages)
        {
            List<string> b64Images = new List<string>();
            List<BasicImage> sortedBasicImages = basicImages.OrderBy(o => o.Position).ToList();
            foreach (BasicImage bi in basicImages)
            {
                string uid = bi.Path;
                var provider = new FileExtensionContentTypeProvider();
                string mime = "";
                provider.TryGetContentType(uid, out mime);
                b64Images.Add("data:" + mime + ";base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(@"c:\temp\" + uid)));
            }

            return b64Images;
        }
    }
}