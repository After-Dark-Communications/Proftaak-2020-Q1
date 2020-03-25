using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DTO;

namespace DAL.Concrete
{
    public class TramAccess : ITramAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;

        public TramAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(TramDTO obj)
        {
            using (_context)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int key)
        {
            using (_context)
            {
                var tram = await _context.Tram.FirstOrDefaultAsync(t => t.Id == key);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<TramDTO> GetAllTrams()
        {
            //return _context.Trams.ToList();
            throw new NotImplementedException();
        }

        public TramDTO Read(int key)
        {
            using (_context)
            {
                TramDTO tram = new TramDTO();
                var readTram = _context.Tram.FirstOrDefault(i => i.Id == key);
                return tram = _mapper.Map<TramDTO>(readTram);
            }
        }

        public async Task Update(TramDTO obj)
        {
            using (_context)
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
