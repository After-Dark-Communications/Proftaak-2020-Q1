using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DTO;
using Services;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            if(!tram.Status.Any(s => s.Status == status.Status))
            {
                tram.Status.Add(status);
                Update(tram);
            }
        }

        public void DeleteStatus(StatusDTO status, TramDTO tram)
        { 

        }

        public bool CheckIfTramExists(string tramNumber)
        {
            return true;
            /*
            if (_tramAccess.ReadFromTramNumber(tramNumber) == null)
            {
                return true;
            }      
            else
            {
                return false;
            }//*/

           
        }

        public void GetServiceHistory()
        {
            throw new NotImplementedException();
        }
        public TramDTO GetTram(int key)
        {
            return _tramAccess.Read(key);
        }

        public TramDTO GetTram(string tramNumber)
        {
            return _tramAccess.ReadFromTramNumber(tramNumber);
        }

        public void Update(TramDTO tram ) // private maken?
        {
            _tramAccess.Update(tram);
        }
    }
}
