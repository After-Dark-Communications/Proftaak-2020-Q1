using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;

namespace Logic
{
    public class Sector
    {
        private readonly ISectorAccess _sectorAccess;
        public Sector(ISectorAccess sectorAcces)
        {
            this._sectorAccess = sectorAcces;
        }
       
        public void AddTram(TramDTO tram, SectorDTO sector)
        {
            sector.Tram = tram;
            _sectorAccess.Update(sector);
        }

    public bool CheckIfSectorIsEmpty(SectorDTO sector)
    {
           if (sector.Tram == null) //not sure about this statement
            {
                return true;
            }
            else
            {
                return false;
            }
    }

    public TramDTO GetTram(SectorDTO sector)
    {
            return sector.Tram;
    }
}
}
