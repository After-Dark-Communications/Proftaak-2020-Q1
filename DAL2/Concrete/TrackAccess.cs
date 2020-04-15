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
        public void Create(TrackDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int key)
        {
            throw new NotImplementedException();
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

        public void Update(TrackDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
