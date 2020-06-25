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
        private readonly IUserAccess _userAccess;
        private readonly User _user;

        public UserCollection(IUserAccess userAccess, User user)
        {
            _userAccess = userAccess;
            _user = user;
        }
        public void RegisterUser(UserDTO user)
        {
            UserDTO model = _user.AlterUserCredentialsToHash(user);
            _userAccess.Create(model);
        }
        //public bool IsAdmin(UserDTO user)
        //{
        //    return user.Permissions.Where(x => x.Name == "Admin").Any();
        //}

        public IEnumerable<UserDTO> GetUsersByPermission(string permission)
        {
            return _userAccess.GetUserByType(permission);
        }
    }
}
