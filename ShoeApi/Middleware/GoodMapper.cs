using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Middleware
{
    public class GoodMapper: Profile
    {
        public GoodMapper()
        {
            CreateMap<GoodVm, Good>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dst => dst.PhotoFileName, opt => opt.MapFrom(src => src.PhotoFileName))
                ;
        }
    }
}