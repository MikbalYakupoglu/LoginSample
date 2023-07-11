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
        Task<IResult> AddRoleToUserAsync(int userId, List<int> roleIds);
        Task<IResult> RemoveRoleFromUserAsync(int userId, List<int> roleIds);

    }
}
