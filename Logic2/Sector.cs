using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;

namespace Logic
{
    public class Sector
    {
        private readonly ISectorAccess _sectoraccess;
        private readonly ITramAccess _tramAccess;
        public Sector(ISectorAccess sectoraccess, ITramAccess tramAcces)
        {
            _sectoraccess = sectoraccess;
            _tramAccess = tramAcces;
        }

        public SectorDTO GetSector(int key)
        {
            var sector =_sectoraccess.Read(key);
            if(sector.Tram != null)
            {
                sector.Tram = _tramAccess.Read(sector.Tram.Id);
            }
            
            return sector;
        }

        public void AddTram(SectorDTO sector, TramDTO tram)
        {
            if(sector.Tram == null)
            {
                sector.Tram = tram;
                _sectoraccess.Update(sector);
            }
        }

        public void RemoveTram(SectorDTO sector)
        {
            _sectoraccess.RemoveTram(sector.Id);
        }

        public void Update(SectorDTO sector)
        {
            _sectoraccess.Update(sector);
        }
        public int GetSectorByTramNumber(string Tramnumber)
        {
            return _sectoraccess.GetSectorIdByTramNumber(Tramnumber);
        }

        public bool CheckIfSectorIsEmpty(SectorDTO sector)
        {
            if (sector.Tram == null)
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
