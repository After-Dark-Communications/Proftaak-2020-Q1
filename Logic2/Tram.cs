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

        public TramDTO GetTram(int key)
        {
            return _tramAccess.Read(key);
        }

        public TramDTO GetTram(string tramNumber)
        {
            return _tramAccess.ReadFromTramNumber(tramNumber);
        }

        public void Update(TramDTO tram )
        {
            _tramAccess.Update(tram);
        }
    }
}
