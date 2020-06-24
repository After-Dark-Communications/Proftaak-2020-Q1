using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Context;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using Services;

namespace DAL.Concrete
{
    public class StatusAccess : IStatusAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;

        public StatusAccess(DepotContext context, Mapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(StatusDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int key)
        {
            throw new NotImplementedException();
        }

        public void Delete(TramDTO tram, TramStatus status)
        {
            throw new NotImplementedException();
        }

        public StatusDTO Read(int key)
        {
            using (_context)
            {
                StatusDTO status = new StatusDTO();
                var readStatus = _context.Status.FirstOrDefault(i => i.StatusId == key);
                return status = _mapper.Map<StatusDTO>(readStatus);
            }
        }

        public void Update(StatusDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
