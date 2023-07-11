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
        Task<IResult> CreateAsync(Category category);
        Task<IResult> DeleteAsync(int categoryId);
        Task<IResult> UpdateAsync(int categoryId, Category newCategory);
        Task<IDataResult<IEnumerable<Category>>> GetAllAsync();
        Task<IDataResult<Category>> GetByIdAsync(int categoryId);

    }
}
