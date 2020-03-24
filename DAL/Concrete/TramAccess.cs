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

namespace DAL.Concrete
{
    public class TramAccess : ITramAccess
    {
        private readonly TramContext _context;
        private readonly IMapper _mapper;

        public TramAccess(TramContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(TramDal obj)
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
                var tram = await _context.Trams.FirstOrDefaultAsync(t => t.Id == key);
                _context.Trams.Remove(tram);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<TramDal> GetAllTrams()
        {
            return _context.Trams.ToList();
        }

        public TramDal Read(int key)
        {
            using (_context)
            {
                TramDal tram = new TramDal();
                var readTram = _context.Trams.FirstOrDefault(i => i.Id == key);
                return tram = _mapper.Map<TramDal>(readTram);
            }
        }

        public async Task Update(TramDal obj)
        {
            using (_context)
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
