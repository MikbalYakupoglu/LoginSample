using Core.DataAccess;
using Core.DataAccess.Extensions;
using Core.Entity;
using DataAccess.Abstract;
using Entity.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, LoginSampleContext> , IUserDal
    {
        public override void Delete(User user)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                user.IsActive = false;

                var userToDelete = context.Entry(user);
                userToDelete.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public override User? Get(Expression<Func<User, bool>> filter)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                return context.Users
                    .Include(u=> u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .SingleOrDefault(filter);
            }
        }

        public override IEnumerable<User> GetAll(Expression<Func<User, bool>> filter = null, int page = 0, int size = 25)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                var users = context.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role);

                if (!users.Any())
                    return Enumerable.Empty<User>();

                return filter == null
                    ? users.ToPaginate(page,size)
                    : users.Where(filter).ToPaginate(page, size);
            }
        }
    }
}
