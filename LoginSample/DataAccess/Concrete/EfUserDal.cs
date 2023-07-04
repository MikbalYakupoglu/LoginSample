using DataAccess.Abstract;
using Entity;
using Entity.Abstract;
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
    public class EfUserDal : IUserDal
    {
        public void Create(User user)
        {
            using(LoginSampleContext context = new LoginSampleContext())
            {
                var userToAdd = context.Entry(user);
                userToAdd.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                return context.Set<User>().SingleOrDefault(filter);
            }
        }

        public IEnumerable<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                return filter == null
                    ? context.Set<User>().ToList()
                    : context.Set<User>().Where(filter).AsNoTrackingWithIdentityResolution().ToList();
            }
        }

        public void Remove(User user)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                var userToAdd = context.Entry(user);
                userToAdd.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Update(User user)
        {
            using (LoginSampleContext context = new LoginSampleContext())
            {
                var userToAdd = context.Entry(user);
                userToAdd.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
