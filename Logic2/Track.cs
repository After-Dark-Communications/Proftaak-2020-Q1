using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DTO;

namespace Logic
{
    public class Track
    {
        private readonly ITrackAccess _trackAccess;

        public Track(ITrackAccess trackAccess)
        {
            _trackAccess = trackAccess;
        }
        public void CheckTramType(TramDTO tram)
        {
            throw new NotImplementedException();
        }
        public TrackDTO Read(int key)
        {
            return _trackAccess.Read(key);
        }
        public void Update(TrackDTO track)
        {
             _trackAccess.Update(track);
        }
    }
}
