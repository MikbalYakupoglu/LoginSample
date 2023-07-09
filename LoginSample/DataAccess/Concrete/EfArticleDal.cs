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
        public override void Delete(Article article)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                article.IsDeleted = true;

                var articleToDelete = context.Entry(article);
                articleToDelete.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public override Article? Get(Expression<Func<Article, bool>> filter)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                return context.Articles
                    .Include(a => a.Creator)
                    .SingleOrDefault(filter);
            }
        }

        public override IEnumerable<Article> GetAll(Expression<Func<Article, bool>> filter = null, int page = 0, int size = 10)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                var articles = context.Articles
                    .Include(a => a.Creator);

                if (!articles.Any())
                    return Enumerable.Empty<Article>();

                return filter == null
                    ? articles.ToPaginate(page, size)
                    : articles.Where(filter).ToPaginate(page, size);
            }
        }
    }
}
