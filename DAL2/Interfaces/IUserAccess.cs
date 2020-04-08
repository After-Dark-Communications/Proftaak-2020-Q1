using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using DTO;

namespace DAL.Interfaces
{
    public interface IUserAccess : IGenAccess<UserDTO>
    {
        UserDTO GetUser(UserDTO account);
    }
}
