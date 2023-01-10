using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
namespace BrokenGrenade.Web.BL.MapperProfiles;

public class PunishmentMapperProfile : Profile
{
    public PunishmentMapperProfile()
    {
        CreateMap<PunishmentEntity, PunishmentModel>()
            .ReverseMap()
            .ForMember(entity => entity.User, opt => opt.Ignore())
            .ForMember(entity => entity.IssuedBy, opt => opt.Ignore());
    }
}
