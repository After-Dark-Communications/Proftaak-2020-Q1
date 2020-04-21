using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        //public List<PermissionViewModel> Permissions;
        public string Permissions { get; set; }
        public UserViewModel()
        {

        }

        public UserViewModel(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        public UserViewModel(string Username, string Name, string Surname, string Password, params string[] Permissions)
        {
            this.UserName = UserName;
            this.Name = Name;
            this.Surname = Surname;
            this.Password = Password;
        }
    }
}
