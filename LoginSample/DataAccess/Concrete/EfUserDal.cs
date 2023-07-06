using Core.DataAccess;
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
                var userToAdd = context.Entry(user);
                userToAdd.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
