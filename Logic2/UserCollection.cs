using System;
using System.Collections.Generic;
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
        public bool RegisterUser()
        {
            throw new NotImplementedException();
        }
        public bool IsAdmin(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
