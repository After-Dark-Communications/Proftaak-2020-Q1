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

        public void Create(TramDTO tram)
        {
            _tramAccess.Create(tram);
        }
        public void Delete(int key)
        {
            _tramAccess.Delete(key);
        }

        public void Delete(TramDTO tram)
        {
            _tramAccess.Delete(tram.Id);
        }

        public void Delete(string tramNumber)
        {
            var tram = _tramAccess.ReadFromTramNumber(tramNumber);
            _tramAccess.Delete(tram.Id);
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

        public TramDTO GetRandomTram()
        {
            Random rnd = new Random();
            List<int> keys = _tramAccess.GetAllTramIds();
            return _tramAccess.Read(keys[rnd.Next(1, keys.Count)]);
        }
    }
}
