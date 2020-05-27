using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL.Interfaces
{
    public interface ISectorAccess : IGenAccess<SectorDTO>
    {
         int GetSectorIdByTramNumber(string TramNumber);
         void RemoveTram(int sectorId);
    }
}
