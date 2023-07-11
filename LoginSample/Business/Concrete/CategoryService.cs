using Business.Abstract;
using Business.Utils;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<IResult> CreateAsync(Category category)
        {
            var result = BusinessRules.Run(
                await CheckIfCategoryNameAlreadyExistAsync(category.Name),
                CheckIfCategoryNameNull(category.Name)
                );

            if (!result.Success)
                return new ErrorResult(result.Message);

            await _categoryDal.CreateAsync(category);
            return new SuccessResult(Messages.CategoryCreateSuccess);

        }

        public async Task<IResult> DeleteAsync(int categoryId)
        {
            var result = BusinessRules.Run(
                 await CheckIfCategoryExistInDb(categoryId)
                );

            if(!result.Success)
                return new ErrorResult(result.Message);

            var categoryToDelete = await _categoryDal.GetAsync(c => c.Id == categoryId);
            await _categoryDal.DeleteAsync(categoryToDelete);
            return new SuccessResult(Messages.CategoryDeleteSuccess);
        }

        public async Task<IResult> UpdateAsync(int categoryId, Category newCategory)
        {
            var result = BusinessRules.Run(
                await CheckIfCategoryExistInDb(categoryId)
            );

            if (!result.Success)
                return new ErrorResult(result.Message);

            var categoryToUpdate = await _categoryDal.GetAsync(c => c.Id == categoryId);
            categoryToUpdate.Name = newCategory.Name;
            await _categoryDal.UpdateAsync(categoryToUpdate);

            return new SuccessResult(Messages.CategoryUpdateSuccess);
        }

        public async Task<IDataResult<IEnumerable<Category>>> GetAllAsync()
        {
            var categories = await _categoryDal.GetAllAsync(null);

            return new SuccessDataResult<IEnumerable<Category>>(categories);

        }

        public async Task<IDataResult<Category>> GetByIdAsync(int categoryId)
        {
            var result = BusinessRules.Run(
                await CheckIfCategoryExistInDb(categoryId)
            );

            if (!result.Success)
                return new ErrorDataResult<Category>(result.Message);

            var category = await _categoryDal.GetAsync(c => c.Id == categoryId);
            return new SuccessDataResult<Category>(category);
        }


        private async Task<IResult> CheckIfCategoryNameAlreadyExistAsync(string categoryName)
        {
            var category = await _categoryDal.GetAsync(c => c.Name == categoryName);

            if (category != null)
                return new ErrorResult(Messages.CategoryAlreadyExist);

            return new SuccessResult();
        }

        private async Task<IResult> CheckIfCategoryExistInDb(int categoryId)
        {
            var category = await _categoryDal.GetAsync(c => c.Id == categoryId);

            if (category == null)
                return new ErrorResult(Messages.CategoryNotFound);

            return new SuccessResult();
        }

        private IResult CheckIfCategoryNameNull(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return new ErrorResult(Messages.CategoryNameCannotBeNull);

            return new SuccessResult();
        }

    }
}
