using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public record UserDto
    {
        public int Id { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
    }
}
