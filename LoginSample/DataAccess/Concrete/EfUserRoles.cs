using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserRoles : IUserRolesDal
    {
        public List<string> GetUserRoles(int userId)
        {
            using (var context = new LoginSampleContext())
            {
                var userWithRoles = context.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefault(u => u.Id == userId);

                List<string> userRoles = new List<string>();

                foreach (var roles in userWithRoles.UserRoles)
                {
                    userRoles.Add(roles.Role.Name);
                }

                return userRoles;
            }
        }
    }
}
