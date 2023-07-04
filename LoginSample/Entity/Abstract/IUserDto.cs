using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Abstract
{
    public interface IUserDto : IUser
    {
        public string Password { get; set; }
    }
}
