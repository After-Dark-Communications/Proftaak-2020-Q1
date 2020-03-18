using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Permission> Permissions;
        public void Login()
        {
            throw new NotImplementedException();
        }
        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
