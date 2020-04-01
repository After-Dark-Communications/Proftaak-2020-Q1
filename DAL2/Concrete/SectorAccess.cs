using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using DAL.Interfaces;
using DTO;
using AutoMapper;

namespace DAL.Concrete
{
    public class SectorAccess : ISectorAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;
        public SectorAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(SectorDTO obj)
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
                var deletedsector = _context.Sector.FirstOrDefault(x => x.Id == key);
                _context.Sector.Remove(deletedsector);
                await _context.SaveChangesAsync();
            }
        }

        public SectorDTO Read(int key)
        {
            using(_context)
            {
                SectorDTO sector = new SectorDTO();
                var readsector = _context.Sector.FirstOrDefault(i => i.Id == key);
                return sector = _mapper.Map<SectorDTO>(readsector);
            }
        }

        public async Task Update(SectorDTO obj)
        {
            using(_context)
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
