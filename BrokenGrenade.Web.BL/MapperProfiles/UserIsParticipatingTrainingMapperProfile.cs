using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class UserIsParticipatingTrainingMapperProfile : Profile
{
    public UserIsParticipatingTrainingMapperProfile()
    {
        CreateMap<UserIsParticipatingTrainingEntity, UserIsParticipatingTrainingModel>()
            .ReverseMap()
            .ForMember(entity => entity.Training, opt => opt.Ignore())
            .ForMember(entity => entity.User, opt => opt.Ignore());
    }
}