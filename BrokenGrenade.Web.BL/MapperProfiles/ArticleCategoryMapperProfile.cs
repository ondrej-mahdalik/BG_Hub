using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.BL.MapperProfiles;

public class ArticleCategoryMapperProfile : Profile
{
    public ArticleCategoryMapperProfile()
    {
        CreateMap<ArticleCategoryEntity, ArticleCategoryModel>()
            .ReverseMap()
            .ForMember(entity => entity.Articles, opt => opt.Ignore())
            .ForMember(entity => entity.Parent, opt => opt.Ignore())
            .ForMember(entity => entity.Children, opt => opt.Ignore());
    }
}