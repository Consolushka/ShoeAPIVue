using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Middleware
{
    public class TypeMapper: Profile
    {
        public TypeMapper()
        {
            CreateMap<TypeVM, Type>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                ;
        }
    }
}