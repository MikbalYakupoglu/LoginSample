﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRolesDal
    {
        List<string> GetUserRoles(int userId);
    }
}
