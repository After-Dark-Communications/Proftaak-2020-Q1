using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Concrete
{
    public class UserAccess : IUserAccess
    {
        public Task Create(User obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int key)
        {
            throw new NotImplementedException();
        }

        public User Read(int key)
        {
            throw new NotImplementedException();
        }

        public Task Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
