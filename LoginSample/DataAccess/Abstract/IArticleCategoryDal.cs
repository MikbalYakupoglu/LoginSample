using Core.DataAccess;
using Entity.Concrete;

namespace DataAccess.Abstract;

public interface IArticleCategoryDal : IEntityRepositoryBase<ArticleCategory>
{
        Task<List<string>> GetArticleCategoriesAsync(int articleId);

}