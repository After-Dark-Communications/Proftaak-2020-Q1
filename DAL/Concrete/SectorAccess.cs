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
        public SectorAccess(DepotContext context)
        {
            _context = context;
        }

        public Task Create(SectorDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int key)
        {
            throw new NotImplementedException();
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

        public Task Update(SectorDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
