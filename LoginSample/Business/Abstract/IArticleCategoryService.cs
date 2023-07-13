using Core.Results;

namespace Business.Abstract;

public interface IArticleCategoryService
{
    Task<IResult> AddCategoryToArticleAsync(int articleId, List<int> categoryIds);
    Task<IResult> RemoveCategoryFromArticleAsync(int articleId, List<int> categoryIds);
}