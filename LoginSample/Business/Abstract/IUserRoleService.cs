using Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserRoleService
    {
        IResult AddRoleToUser(int userId, List<int> roleIds);
        IResult RemoveRoleFromUser(int userId, List<int> roleIds);

    }
}
