using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class MissionTypeMapperProfile : Profile
{
    public MissionTypeMapperProfile()
    {
        CreateMap<MissionTypeEntity, MissionTypeModel>()
            .ReverseMap()
            .ForMember(entity => entity.Missions, opt => opt.Ignore());
    }
}