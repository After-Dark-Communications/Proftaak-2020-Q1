using AutoMapper;
using Context;
using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class TrackAccess : ITrackAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;

        public TrackAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task Create(TrackDTO obj)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int key)
        {
            using(_context)
            {
                var tram = await _context.Track.FirstOrDefaultAsync(t => t.Id == key);
                _context.Track.Remove(tram);
                await _context.SaveChangesAsync();

            }
        }

        public TrackDTO Read(int key)
        {
            using(_context)
            {
                TrackDTO track = new TrackDTO();
                var readtrack = _context.Track.FirstOrDefault(i => i.Id == key);
                return track = _mapper.Map<TrackDTO>(readtrack);
            }
        }

        public async Task Update(TrackDTO obj)
        {
            using(_context)
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
