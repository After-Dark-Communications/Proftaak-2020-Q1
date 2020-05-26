using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ITrackAccess : IGenAccess<TrackDTO>
    {
        public TrackDTO ReadTrackByTramNumber(string TramNumber);
    }
}
