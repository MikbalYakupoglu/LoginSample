using Core.DataAccess;
using Core.DataAccess.Extensions;
using Core.Entity;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class EfArticleDal : EfEntityRepositoryBase<Article, LoginSampleContext>, IArticleDal
    {
        public override async Task DeleteAsync(Article article)
        {
            await using (LoginSampleContext context = new LoginSampleContext())
            {
                article.IsDeleted = true;

                var articleToDelete = context.Entry(article);
                articleToDelete.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public override async Task<Article?> GetAsync(Expression<Func<Article, bool>> filter)
        {
            await using (LoginSampleContext context = new LoginSampleContext())
            {
                return await context.Articles
                    .Include(a => a.Creator)
                    .SingleOrDefaultAsync(filter);
            }
        }

        public override async Task<IEnumerable<Article>> GetAllAsync(Expression<Func<Article, bool>>? filter = null, int page = 0, int size = 10)
        {
            await using (LoginSampleContext context = new LoginSampleContext())
            {
                var articles = context.Articles
                    .Include(a => a.Creator)
                    .OrderByDescending(filter => filter.CreatedAt);

                if (!articles.Any())
                    return Enumerable.Empty<Article>();

                return filter == null
                    ? await articles.ToPaginateAsync(page, size)
                    : await articles.Where(filter).ToPaginateAsync(page, size);
            }
        }
    }
}
