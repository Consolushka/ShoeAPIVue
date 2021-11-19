using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Middleware
{
    public class BrandMapper: Profile
    {
        public BrandMapper()
        {
            CreateMap<BrandVM, Brand>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                ;
        }
    }
}