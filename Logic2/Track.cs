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
        Sector _sectorLogic;

        public Track(ITrackAccess trackAccess, Sector sectorlogic)
        {
            _trackAccess = trackAccess;
            this._sectorLogic = sectorlogic;
        }
        
        private bool CheckTramType(TramDTO tram, TrackDTO track)
        {
            return true;
            if (tram.Type == track.TramType || track.TramType == null)
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
                    _sectorLogic.AddTram(sector, tram);
                    break;
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

        public TrackDTO GetRandomTrack(List<TrackDTO> tracks)
        {
            Random random = new Random();

            int amountOfTracks = tracks.Count;
            int randomTrack = random.Next(1, 8);
            return tracks[randomTrack];
        }
    }
}
