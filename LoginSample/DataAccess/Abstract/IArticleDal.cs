using Core.DataAccess;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IArticleDal : IEntityRepositoryBase<Article>
    {
        Task<IEnumerable<Article>> GetAllByCategory(string categoryName, int page, int size);
    }
}
