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
        public string Permission { get; set; }

        public UserDTO()
        {
                
        }

        public UserDTO(int id, string userName, string password, string name, string surname, string permission)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Name = name;
            Surname = surname;
            Permission = permission;    
        }

        public UserDTO(string name)
        {
            Name = name;
        }

    }
}
