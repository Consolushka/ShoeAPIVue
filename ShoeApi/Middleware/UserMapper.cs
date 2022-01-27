using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Middleware
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<UserVM, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                ;
            
            CreateMap<User, UserResponse>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
                .ForMember(dst => dst.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;
        }
    }
}