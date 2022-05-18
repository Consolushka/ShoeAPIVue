using AutoMapper;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Type = Shop.Data.Models.Type;

namespace Shop.MiddleWare
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<BrandVM, Brand>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
            
            
            CreateMap<GoodVm, Good>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dst => dst.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dst => dst.PhotoFileName, opt => opt.MapFrom(src => src.PhotoFileName));
            
            
            CreateMap<TypeVM, Type>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
            
            CreateMap<UserVM, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password));
            
            CreateMap<User, UserResponse>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
                .ForMember(dst => dst.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Token, opt => opt.Ignore());
        }
    }
}