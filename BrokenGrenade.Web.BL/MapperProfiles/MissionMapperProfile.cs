using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class MissionMapperProfile : Profile
{
    public MissionMapperProfile()
    {
        CreateMap<MissionEntity, MissionModel>()
            .ReverseMap()
            .ForMember(entity => entity.Creator, opt => opt.Ignore())
            .ForMember(entity => entity.MissionType, opt => opt.Ignore())
            .ForMember(entity => entity.ModpackType, opt => opt.Ignore());
    }
}