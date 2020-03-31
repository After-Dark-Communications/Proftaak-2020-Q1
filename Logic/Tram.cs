using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DTO;
using Logic.Models;

namespace Logic
{
    public class Tram {

        private readonly ITramAccess _tramAccess;

        public Tram(ITramAccess tramAccess)
        {
            _tramAccess = tramAccess;
        }

        public void AddStatus(TramDTO tram, TramStatus newStat)
        {

        }

        public void DeleteStatus(TramStatus stat)
        {
            //Status.Remove(stat);

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
