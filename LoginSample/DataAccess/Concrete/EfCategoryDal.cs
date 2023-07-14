using Core.DataAccess;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, LoginSampleContext>, ICategoryDal
    {
        public async Task<List<int>> ConvertCategoryNamesToCategoryIdsAsync(IEnumerable<string> categoryNames)
        {
            await using (LoginSampleContext context = new LoginSampleContext())
            {
                var categories = await context.Categories.ToListAsync();
                List<int> categoryIds = new List<int>();
                foreach (var categoryName in categoryNames)
                {
                    int categoryId = categories.Where(c => c.Name == categoryName).Select(c=> c.Id).SingleOrDefault();
                    categoryIds.Add(categoryId);
                }

                return categoryIds;
            }
        }
    }
}
