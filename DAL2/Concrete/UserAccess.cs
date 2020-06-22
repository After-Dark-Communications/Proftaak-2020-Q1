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
using DAL.Context;
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

        public UserDTO Read(int key)
        {
            using(_context)
            {
                UserDTO user = new UserDTO();
                var readuser = _context.User.FirstOrDefault(x => x.Id == key);
                return user = _mapper.Map<UserDTO>(readuser);
            }
        }

        public UserDTO Get(UserDTO user)
        {
            UserDTO EmptyDTO = new UserDTO();

            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id, UserName, Name, Surname, Password, Permission FROM [User] " +
                "WHERE [User].UserName = @UserName AND [User].Password = @Password ", conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("UserName", user.UserName);
                    command.Parameters.AddWithValue("Password", user.Password);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int Id = reader.GetInt32(0);
                        string UserName = reader.GetString(1);
                        string Name = reader.GetString(2);
                        string Surname = reader.GetString(3);
                        string Password = reader.GetString(4);
                        string Permission = reader.GetString(5);
                        UserDTO UserData = new UserDTO(Id, UserName, Password, Name, Surname, Permission);

                        return UserData;
                    }

                    conn.Close();
                    return EmptyDTO;
                }
            }
        }
        public UserDTO GetUserByUsername(string Username)
        {
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                UserDTO Emptyuser = new UserDTO();
                using(SqlCommand cmd = new SqlCommand("SELECT Id, UserName, Name, Surname, Password, Permission FROM [User] " +
                "WHERE [User].UserName = @UserName", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("Username", Username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string UserName = reader.GetString(1);
                        string Name = reader.GetString(2);
                        string Surname = reader.GetString(3);
                        string Password = reader.GetString(4);
                        string Permission = reader.GetString(5);
                        UserDTO UserData = new UserDTO(id, UserName, Password, Name, Surname, Permission);

                        return UserData;

                    }
                    conn.Close();
                }
                return Emptyuser;
            }
        }
        public void Create(UserDTO obj)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO [User] (Username, Name, Surname, Password, Permission) Values(@Username, @Name, @Surname, @Password, @Permission)", conn))
                {
                    command.Parameters.Add(new SqlParameter("UserName", obj.UserName));
                    command.Parameters.Add(new SqlParameter("Name", obj.Name));
                    command.Parameters.Add(new SqlParameter("Surname", obj.Surname));
                    command.Parameters.Add(new SqlParameter("Password", obj.Password));
                    command.Parameters.Add(new SqlParameter("Permission", obj.Permission));
                    command.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        void IGenAccess<UserDTO>.Update(UserDTO obj)
        {
            throw new NotImplementedException();
        }

        void IGenAccess<UserDTO>.Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
