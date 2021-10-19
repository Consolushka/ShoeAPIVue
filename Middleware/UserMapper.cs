﻿using AutoMapper;
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
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;
            
            CreateMap<User, AuthenticateResponse>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;
        }
    }
}