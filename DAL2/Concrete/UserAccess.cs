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
        public void Create(UserDTO obj)
        {
            using(_context)
            {
                try
                {
                    _context.Add(obj);
                    _context.SaveChanges();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(int key)
        {
            using (_context)
            {
                var deleteduser = _context.User.FirstOrDefault(x => x.Id == key);
                _context.User.Remove(deleteduser);
                _context.SaveChanges();
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

        public void Update(UserDTO obj)
        {
            using(_context)
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
        }
    }
}
