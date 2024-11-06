using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class User : Core.Entities.Entity
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
    public class UserDto : Dto
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
