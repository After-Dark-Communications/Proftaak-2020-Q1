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
        public Sector(ISectorAccess sectoraccess)
        {
            _sectoraccess = sectoraccess;
        }
        public void AddTram(SectorDTO sector, TramDTO tram)
        {
            sector.Tram = tram;
            _sectoraccess.Update(sector);

        }
    }
}
