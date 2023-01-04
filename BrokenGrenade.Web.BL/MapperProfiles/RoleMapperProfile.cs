using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class RoleMapperProfile : Profile
{
    public RoleMapperProfile()
    {
        CreateMap<RoleEntity, RoleModel>()
            .ForMember(model => model.CreateMissions, opt => opt.Ignore())
            .ForMember(model => model.ManageMissions, opt => opt.Ignore())
            .ForMember(model => model.ManageUsers, opt => opt.Ignore())
            .ForMember(model => model.ManageRoles, opt => opt.Ignore())
            .ForMember(model => model.ManageMissionTypes, opt => opt.Ignore())
            .ForMember(model => model.ManageModpackTypes, opt => opt.Ignore())
            .ReverseMap();
    }
}