using AutoMapper;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Helpers.Mapper;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator.Email))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ArticleCategories.Select(ac => ac.Category.Name)));

        CreateMap<ArticleDto, Article>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.ArticleCategories, opt => opt.MapFrom(src => src.Categories.Select(categoryName => new ArticleCategory { Category = new Category { Name = categoryName } }).ToList()));

    }
}