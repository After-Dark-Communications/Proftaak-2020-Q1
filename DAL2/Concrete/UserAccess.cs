using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using DAL.Interfaces;
using DAL.Models;
using AutoMapper;
using Context;
using System.Linq;
using DTO;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.Concrete
{
    public class UserAccess : IUserAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;

        public UserAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(UserDTO obj)
        {
            //using (SqlConnection conn = new SqlConnection(DbConnection.connection))
            //{
            //    using (SqlCommand command = new SqlCommand("INSERT INTO [User] (Username, Name, Surname, Password) Values(@Username, @Name, @Surname, @Password)", conn))
            //    {
            //        conn.Open();     
            //          command.Parameters.Add(new SqlParameter("UserName", obj.UserName));
            //          command.Parameters.Add(new SqlParameter("Name", obj.Name));
            //          command.Parameters.Add(new SqlParameter("Surname", obj.Surname));
            //          command.Parameters.Add(new SqlParameter("Password", obj.Password));
            //        conn.Close();
            //        
            //    }

            //using (SqlCommand command = new SqlCommand("INSERT INTO [User_Permission] (PermissionId, UserId) Values((Select Permission.Id Where Permission.Name= @PermissionName), (Select User.Id Where User.Name= @UserName))", conn))
            //    {
            //        conn.Open();     
            //          foreach(var permission in obj.Permissions)
            //{
            //    command.Parameters.Add(new SqlParameter("PermissionName", permission.Name));
            //    command.Parameters.Add(new SqlParameter("UserName", obj.Name));

            //}
            //        conn.Close();
            //       
            //    }
            //}
        }

        public async Task Delete(int key)
        {
            using (_context)
            {
                var deleteduser = _context.User.FirstOrDefault(x => x.Id == key);
                _context.User.Remove(deleteduser);
                await _context.SaveChangesAsync();
            }
        }

        public UserDTO Read(int key)
        {
            using(_context)
            {
                UserDTO user = new UserDTO();
                var readuser = _context.User.FirstOrDefault(x => x.Id == key);
                return user = _mapper.Map<UserDTO>(readuser);
            }
        }

        public async Task Update(UserDTO obj)
        {
            using(_context)
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
