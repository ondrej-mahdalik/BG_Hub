using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserEntity, UserModel>()
            //.ForMember(model => model.Roles, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(entity => entity.MissionsCreated, opt => opt.Ignore())
            .ForMember(entity => entity.Email, opt => opt.AddTransform(value => value!.Trim()))
            .ForMember(entity => entity.UserName, opt => opt.MapFrom(model => model.Email.Trim()));
    }
}