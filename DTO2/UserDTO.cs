using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<PermissionDTO> Permissions;

        public UserDTO()
        {
                
        }

        public UserDTO(string userName, string password, string name, string surname)
        {
            UserName = userName;
            Password = password;
            Name = name;
            Surname = surname;  
        }
    }
}
