﻿using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DTO;
using Services;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Logic
{
    public class Tram 
    {

        private readonly ITramAccess _tramAccess;
        private readonly IStatusAccess _statusAccess;
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

        public bool CheckIfTramExists(TramDTO tram)
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
      
        public void Create(TramDTO tram)
        {
            _tramAccess.Create(tram);
        }
        public int GetTramIdFromNumber(string number)
        {
            return _tramAccess.GetKeyFromTramNumber(number); 
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

        public TramDTO GetRandomTram()
        {
            Random rnd = new Random();
            List<int> keys = _tramAccess.GetAllTramIds();
            return _tramAccess.Read(keys[rnd.Next(1, keys.Count)]);
        }

        public bool IsTramAllreadyInDepot(string tramNumber)
        {
            return _tramAccess.GetSectorIdFromTram(_tramAccess.GetKeyFromTramNumber(tramNumber)) > 0;
        }

        public void DeleteStatus(TramDTO tram, TramStatus status)
        {
            _tramAccess.DeleteStatus(status, tram);
        }
    }
}
