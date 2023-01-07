using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class ArticleMapperProfile : Profile
{
    public ArticleMapperProfile()
    {
        CreateMap<ArticleEntity, ArticleModel>()
            .ReverseMap()
            .ForMember(entity => entity.Category, opt => opt.Ignore());
    }
}