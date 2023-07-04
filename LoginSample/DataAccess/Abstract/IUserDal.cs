using Entity;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal
    {
        void Create(User user);
        void Remove(User user);
        void Update(User user);
        User Get(Expression<Func<User, bool>> filter);
        IEnumerable<User> GetAll(Expression<Func<User, bool>> filter = null);
    }
}
