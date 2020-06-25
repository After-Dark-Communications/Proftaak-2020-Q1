using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using DTO;

namespace DAL.Interfaces
{
    public interface IUserAccess : IGenAccess<UserDTO>
    {
        UserDTO Get(UserDTO user);
        IEnumerable<UserDTO> GetUserByType(string Permission);
    }
}
