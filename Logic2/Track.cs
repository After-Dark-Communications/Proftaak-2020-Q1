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
        Sector _sectorLogic;
        public Track(Sector sectorlogic)
        {
            this._sectorLogic = sectorlogic;
        }
        private bool CheckTramType(TramDTO tram, TrackDTO track)
        {
            if (tram.Type == track.TramType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckTramCanBeStored(TramDTO tram, TrackDTO track)
        {
            if (!CheckTramType(tram, track))
            {
                return false;
            }
            foreach (SectorDTO sector in track.Sectors)
            {
                if (_sectorLogic.CheckIfSectorIsEmpty(sector))
                {
                    return true;
                }
            }
            return false;
        }

        public void StoreTram(TramDTO tram, TrackDTO track)
        {
            
            foreach (SectorDTO sector in track.Sectors)
            {
                if (_sectorLogic.CheckIfSectorIsEmpty(sector))
                {
                    _sectorLogic.AddTram(tram, sector);
                }
            }
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
