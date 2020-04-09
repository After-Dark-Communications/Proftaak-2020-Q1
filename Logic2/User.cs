using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;

namespace Logic
{
    public class User
    {
        private readonly IUserAccess _userAcces;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Permission> Permissions;

        public User(IUserAccess userAccess)
        {
            _userAcces = userAccess;
        }
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
