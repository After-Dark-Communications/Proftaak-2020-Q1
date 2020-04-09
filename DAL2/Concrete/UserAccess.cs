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
                try
                {
                    _context.Add(obj);
                    await _context.SaveChangesAsync();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
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
    }
}
