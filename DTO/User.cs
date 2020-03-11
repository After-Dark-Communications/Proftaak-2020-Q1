using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class User
    {
        public string UserName  { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Permission> Permissions;
    }
}
