using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DTO;
using Services;
using System.Linq;

namespace Logic
{
    public class Tram {

        private readonly ITramAccess _tramAccess;
        public Tram(ITramAccess tramAccess)
        {
            _tramAccess = tramAccess;
        }

        public void AddStatus(StatusDTO status, TramDTO tram)
        {
            if(tram.Status.Any(s => s.Status == status.Status))
            {
                tram.Status.Add(status);
                _tramAccess.Update(tram);
            }
        }
        public void DeleteStatus(StatusDTO status, TramDTO tram)
        { 

        }

        public void GetType()
        {
            throw new NotImplementedException();
        }

        public void GetServiceHistory()
        {
            throw new NotImplementedException();
        }
        public TramDTO GetTram(int key)
        {
            return _tramAccess.Read(key);
        }
    }
}
