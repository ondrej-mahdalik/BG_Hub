using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<ApplicationEntity, ApplicationModel>()
            .ReverseMap()
            .ForMember(entity => entity.User, opt => opt.Ignore())
            .ForMember(entity => entity.EditedBy, opt => opt.Ignore());
    }
}