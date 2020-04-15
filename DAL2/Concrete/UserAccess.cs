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
using Microsoft.Data.SqlClient;
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

        public void CreateUser(UserDTO obj)
        {
            //using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand("INSERT INTO [User] (Username, Name, Surname, Password) Values(@Username, @Name, @Surname, @Password)", conn))
            //    {
                    
            //        command.Parameters.Add(new SqlParameter("UserName", obj.UserName));
            //        command.Parameters.Add(new SqlParameter("Name", obj.Name));
            //        command.Parameters.Add(new SqlParameter("Surname", obj.Surname));
            //        command.Parameters.Add(new SqlParameter("Password", obj.Password));
            //    }

            //    //using (SqlCommand command = new SqlCommand("INSERT INTO [User_Permission] (PermissionId, UserId) Values((Select Permission.Id Where Permission.Name= @PermissionName), (Select User.Id Where User.Name= @UserName))", conn))
            //    //{
                    
            //    //    foreach (var permission in obj.Permissions)
            //    //    {
            //    //        command.Parameters.Add(new SqlParameter("PermissionName", permission.Name));
            //    //        command.Parameters.Add(new SqlParameter("UserName", obj.Name));

            //    //    }
            //    //}
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

        Task IGenAccess<UserDTO>.Create(UserDTO obj)
        {
            throw new NotImplementedException();
        }

        public UserDTO Get(UserDTO user)
        {
            UserDTO EmptyDTO = new UserDTO();

            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT [User].UserName, [User].Name, [User].Surname, [User].Password, Permission.Name, Permission.Description" +
                "FROM[dbi384571].[dbo].[User]" +
                "INNER JOIN User_Permission On[User].Id = User_Permission.UserId" +
                "INNER JOIN Permission on User_Permission.PermissionId = Permission.Id" +
                "WHERE[User].UserName = @UserName AND[User].Password = @Password", conn))
                {
                    command.Parameters.AddWithValue("UserName", user.UserName);
                    command.Parameters.AddWithValue("Password", user.Password);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string UserName = reader.GetString(0);
                        string Name = reader.GetString(1);
                        string Surname = reader.GetString(2);
                        string Password = reader.GetString(3);
                        string permission = reader.GetString(4);
                        string description = reader.GetString(5);
                        UserDTO UserData = new UserDTO(UserName, Password, Name, Surname);
                        UserData.Permissions.Add(new PermissionDTO(permission, description));
                        return UserData;
                    }

                    return EmptyDTO;
                }
            }
        }

        public UserDTO Get()
        {
            throw new NotImplementedException();
        }
    }
}
