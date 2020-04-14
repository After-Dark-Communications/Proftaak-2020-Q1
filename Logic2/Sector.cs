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
        public TramDTO tramOnLocation; // dit gaat weg zodra de sql statements komen
        public void AddTram(TramDTO tram, SectorDTO sector)
        {
            sector.Tram = tram;
            _sectorAccess.Update(sector);
        }

        public void removeTram()
        {
            this.tramOnLocation = null;
        }


    public bool CheckIfSectorIsEmpty()
    {
            if (tramOnLocation == null)
            {
                return true;
            }
            else
            {
                return false;
            }
    }

    public TramDTO GetTram()
    {
            return tramOnLocation;
    }

    public void testMapMethod2(Tram tram)
        {
            //tram -> tramDTO
        }
}
}
