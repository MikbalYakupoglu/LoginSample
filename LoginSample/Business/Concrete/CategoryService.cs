using Business.Abstract;
using Business.Utils;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Create(Category category)
        {
            var result = BusinessRules.Run(
                CheckIfCategoryNameAlreadyExist(category.Name),
                CheckIfCategoryNameNull(category.Name)
                );

            if (!result.Success)
                return new ErrorResult(result.Message);

            _categoryDal.Create(category);
            return new SuccessResult(Messages.CategoryCreateSuccess);

        }

        public IResult Delete(int categoryId)
        {
            var result = BusinessRules.Run(
                CheckIfCategoryExistInDb(categoryId)
                );

            if(!result.Success)
                return new ErrorResult(result.Message);

            var categoryToDelete = _categoryDal.Get(c => c.Id == categoryId);
            _categoryDal.Delete(categoryToDelete);
            return new SuccessResult(Messages.CategoryDeleteSuccess);
        }

        public IDataResult<IEnumerable<Category>> GetAll()
        {
            var categories = _categoryDal.GetAll(null);

            return new SuccessDataResult<IEnumerable<Category>>(categories);

        }

        public IDataResult<Category> GetById(int categoryId)
        {
            var result = BusinessRules.Run(
                CheckIfCategoryExistInDb(categoryId)
            );

            if (!result.Success)
                return new ErrorDataResult<Category>(result.Message);

            var category = _categoryDal.Get(c => c.Id == categoryId);
            return new SuccessDataResult<Category>(category);
        }

        public IResult Update(int categoryId, Category newCategory)
        {
            var result = BusinessRules.Run(
                CheckIfCategoryExistInDb(categoryId)
                );

            if (!result.Success)
                return new ErrorResult(result.Message);

            var categoryToUpdate = _categoryDal.Get(c => c.Id == categoryId);
            categoryToUpdate.Name = newCategory.Name;
            _categoryDal.Update(categoryToUpdate);

            return new SuccessResult(Messages.CategoryUpdateSuccess);
        }

        private IResult CheckIfCategoryNameAlreadyExist(string categoryName)
        {
            var category = _categoryDal.Get(c => c.Name == categoryName);

            if (category != null)
                return new ErrorResult(Messages.CategoryAlreadyExist);

            return new SuccessResult();
        }

        private IResult CheckIfCategoryExistInDb(int categoryId)
        {
            var category = _categoryDal.Get(c => c.Id == categoryId);

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
