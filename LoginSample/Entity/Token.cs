using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Token
    {
        public int UserId { get; set; }
        public string token { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
