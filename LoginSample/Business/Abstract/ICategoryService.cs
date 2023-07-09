using Core.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IResult Create(Category category);
        IResult Delete(int categoryId);
        IResult Update(int categoryId, Category newCategory);
        IDataResult<IEnumerable<Category>> GetAll();
        IDataResult<Category> GetById(int categoryId);

    }
}
