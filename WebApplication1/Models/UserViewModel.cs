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
        [Required, MinLength(6)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Permission { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        public UserViewModel(string Username, string Name, string Surname, string Password, string Permission)
        {
            this.UserName = UserName;
            this.Name = Name;
            this.Surname = Surname;
            this.Password = Password;
            this.Permission = Permission;
        }
    }
}
