using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL.Interfaces
{
    public interface IDepotAccess : IGenAccess<DepotDTO>
    {
        IEnumerable<TrackDTO> GetAllTracks();
    }
}
