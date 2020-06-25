using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, MinLength(5)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Permission { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public UserViewModel(string userName, string name, string surname, string password, string permission)
        {
            this.UserName = userName;
            this.Name = name;
            this.Surname = surname;
            this.Password = password;
            this.Permission = permission;
        }
    }
}
