using Core.DataAccess;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class EfArticleCategoryDal : EfEntityRepositoryBase<ArticleCategory, LoginSampleContext>, IArticleCategoryDal
{
    public async  Task<List<string>> GetArticleCategoriesAsync(int articleId)
    {
        await using (var context = new LoginSampleContext())
        {
            var articlesWithCategories = await context.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            List<string> articleCategories = new List<string>();
            foreach (var category in articlesWithCategories.ArticleCategories)
            {
                articleCategories.Add(category.Category.Name);
            }

            return articleCategories;
        }
    }
}