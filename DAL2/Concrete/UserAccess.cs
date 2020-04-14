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
            using(_context)
            { 
               await _context.AddAsync(obj); 
               await _context.SaveChangesAsync();
            }
        }

        public void CreateUser(UserDTO obj)
        {
            using (_context)
            {
                User user = _mapper.Map<User>(obj);
                _context.Add(user);
                _context.SaveChanges();
            }
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

        public UserDTO GetUser(UserDTO account)
        {
            using (_context)
            {
                UserDTO user = new UserDTO();
                var getuser = _context.User
                    .Single(s => s.Username == account.UserName && s.Password == account.Password);
                return user = _mapper.Map<UserDTO>(getuser);
            }
        }
    }
}
