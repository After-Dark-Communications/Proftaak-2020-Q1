using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using DTO;

namespace Logic
{
    public class UserCollection
    {
        private readonly IUserAccess _userAcces;

        public UserCollection(IUserAccess userAccess)
        {
            _userAcces = userAccess;
        }
        public void RegisterUser(UserDTO user)
        {
            _userAcces.Create(user);
        }
        public bool IsAdmin(UserDTO user)
        {
            return user.Permissions.Where(x => x.Name == "Admin").Any();
        }
    }
}
