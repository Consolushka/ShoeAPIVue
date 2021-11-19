using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Middleware
{
    public class ShoeMapper: Profile
    {
        public ShoeMapper()
        {
            CreateMap<ShoeVM, Shoe>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dst => dst.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
                .ForMember(dst => dst.PhotoFileName, opt => opt.MapFrom(src => src.PhotoFileName))
                ;
        }
    }
}