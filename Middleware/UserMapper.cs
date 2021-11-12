using AutoMapper;
using Entities.Models;
using Entities.Support;

namespace Middleware
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<UserModel, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dst => dst.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;
            
            CreateMap<User, AuthenticateResponse>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;
        }
    }
}