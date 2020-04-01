using AutoMapper;
using Context;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DAL.Concrete
{
    public class DepotAccess : IDepotAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;
        public DepotAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(DepotDTO obj)
        {
            using(_context)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int key)
        {
            using(_context)
            {
                var deletedDepot = _context.Depot.FirstOrDefault(x => x.Id == key);
                try {
                    _context.Depot.Remove(deletedDepot);
                    await _context.SaveChangesAsync();
                }
                catch(DbException ex)
                {
                    throw ex;
                }
            }
        }

        public DepotDTO Read(int key)
        {
            using(_context)
            {
                DepotDTO depot = new DepotDTO();
                var readdepot = _context.Depot.FirstOrDefault(x => x.Id == key);
                return depot = _mapper.Map<DepotDTO>(readdepot);
            }
        }

        public async Task Update(DepotDTO obj)
        {
            using(_context)
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
