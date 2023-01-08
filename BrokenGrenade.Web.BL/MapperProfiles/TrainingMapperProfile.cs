using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class TrainingMapperProfile : Profile
{
    public TrainingMapperProfile()
    {
        CreateMap<TrainingEntity, TrainingModel>()
            .ReverseMap()
            .ForMember(entity => entity.Creator, opt => opt.Ignore())
            .ForMember(entity => entity.Participants, opt => opt.Ignore());
    }
}