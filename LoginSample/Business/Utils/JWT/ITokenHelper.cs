using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils.JWT
{
    public interface ITokenHelper
    {
        Token CreateToken(User user);
    }
}
