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
        public Task Create(UserDAL obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int key)
        {
            throw new NotImplementedException();
        }

        public UserDAL Read(int key)
        {
            throw new NotImplementedException();
        }

        public Task Update(UserDAL obj)
        {
            throw new NotImplementedException();
        }
    }
}
