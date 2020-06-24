using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DAL.Interfaces;
using DTO;

namespace Logic
{
    public class User
    {
        private readonly IUserAccess _userAccess;
        private readonly IMapper _mapper;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Permission> Permissions;

        public User(IUserAccess userAccess, IMapper mapper)
        {
            _userAccess = userAccess;
            _mapper = mapper;
        }

        public UserDTO Login(UserDTO user)
        {
            UserDTO HashUser = AlterUserCredentialsToHash(user);
            UserDTO resultmodel = _userAccess.Get(HashUser);

            if (CheckIfAccountDataIsEmpty(resultmodel))
            {
                UserDTO result = _mapper.Map<UserDTO>(resultmodel);
                return result;
            }

            return null;
        }

        //hashing user data
        public UserDTO AlterUserCredentialsToHash(UserDTO userCredentials)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(userCredentials.Password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                userCredentials.Password = builder.ToString();
                return userCredentials;
            }
        }

        private bool CheckIfAccountDataIsEmpty(UserDTO userDTO)
        {
            if (userDTO.UserName == null && userDTO.Password == null)
            {
                return false;
            }
            return true;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
