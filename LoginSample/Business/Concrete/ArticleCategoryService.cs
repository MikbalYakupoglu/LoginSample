using Business.Abstract;
using Business.Utils;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.Concrete;

namespace Business.Concrete;

public class ArticleCategoryService : IArticleCategoryService
{
    private readonly IArticleCategoryDal _articleCategoryDal;
    private readonly ICategoryDal _categoryDal;

    public ArticleCategoryService(IArticleCategoryDal articleCategoryDal, ICategoryDal categoryDal)
    {
        _articleCategoryDal = articleCategoryDal;
        _categoryDal = categoryDal;
    }

    public async Task<IResult> AddCategoryToArticleAsync(int articleId, List<int> categoryIds)
    {
        var result = BusinessRules.Run(
            await CheckIfCategoriesExistAsync(categoryIds)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        int addedCategoryCount = await AddArticleCategoryAsync(articleId, categoryIds);

        if (addedCategoryCount == 0)
            return new SuccessResult(Messages.ArticleCategoryNotModified);

        return new SuccessResult(Messages.ArticleCategoryUpdateSuccess);
    }

    public async Task<IResult> RemoveCategoryFromArticleAsync(int articleId, List<int> categoryIds)
    {
        int removedCategoryCount = await RemoveArticleCategoryAsync(articleId, categoryIds);

        if (removedCategoryCount == 0)
            return new SuccessResult(Messages.ArticleCategoryNotModified);

        return new SuccessResult(Messages.ArticleCategoryUpdateSuccess);
    }



    private async Task<int> AddArticleCategoryAsync(int articleId, List<int> categoryIdsToAdd)
    {
        var articleCategories = await _articleCategoryDal.GetAllAsync(ac => ac.ArticleId == articleId);
        var articleCategoryIds = articleCategories.Select(ar=> ar.CategoryId).ToList();
        int addedRoleCount = 0;

        foreach (var categoryToAdd in categoryIdsToAdd)
        {
            if (!articleCategoryIds.Contains(categoryToAdd))
            {
                await _articleCategoryDal.CreateAsync(new ArticleCategory()
                {
                    ArticleId = articleId,
                    CategoryId = categoryToAdd
                });
                addedRoleCount++;
            }
        }

        return addedRoleCount;
    }


    private async Task<int> RemoveArticleCategoryAsync(int articleId, List<int> categoryIdsToRemove)
    {
        var articleCategories = await _articleCategoryDal.GetAllAsync(ac => ac.ArticleId == articleId);
        var articleCategoryIds = articleCategories.Select(ar => ar.CategoryId).ToList();
        int deletedRoleCount = 0;

        foreach (var categoryToRemove in categoryIdsToRemove)
        {
            if (articleCategoryIds.Contains(categoryToRemove))
            {
                var userRoleToDelete = await _articleCategoryDal
                    .GetAsync(ac => ac.ArticleId == articleId && ac.CategoryId == categoryToRemove);
                await _articleCategoryDal.DeleteAsync(userRoleToDelete);
                deletedRoleCount++;
            }
        }
        return deletedRoleCount;
    }


    private async Task<IResult> CheckIfCategoriesExistAsync(List<int> categoryIdsToAdd)
    {
        var categories = await _categoryDal.GetAllAsync(null);
        var categoryIds = categories.Select(r => r.Id);
        var isCategoriesCorrect = categoryIdsToAdd.All(id => categoryIds.Contains(id));

        if (!isCategoriesCorrect)
            return new ErrorResult(Messages.CategoryNotFound);

        return new SuccessResult();
    }
}