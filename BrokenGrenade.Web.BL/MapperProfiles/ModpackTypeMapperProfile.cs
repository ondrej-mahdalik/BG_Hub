using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class ModpackTypeMapperProfile : Profile
{
    public ModpackTypeMapperProfile()
    {
        CreateMap<ModpackTypeEntity, ModpackTypeModel>()
            .ReverseMap()
            .ForMember(entity => entity.Missions, opt => opt.Ignore());
    }
}