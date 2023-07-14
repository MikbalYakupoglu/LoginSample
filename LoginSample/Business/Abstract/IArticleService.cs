using Core.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract;

public interface IArticleService
{
    Task<IResult> CreateAsync(ArticleDto article);
    Task<IResult> DeleteAsync(int articleId);
    Task<IResult> UpdateAsync(int articleId, ArticleDto updatedArticle);
    Task<IDataResult<ArticleDto>> GetAsync(int articleId);
    Task<IDataResult<IEnumerable<ArticleDto>>> GetAllAsync(int page, int size);
    Task<IDataResult<IEnumerable<ArticleDto>>> GetAllByCategoryAsync(string categoryName, int page, int size);

}