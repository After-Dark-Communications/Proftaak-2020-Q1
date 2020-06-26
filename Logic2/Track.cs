using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using DTO;
using Services;


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
            /*if (tram.Type == track.TramType || track.TramType == null);
            {
                return true;
            }
                return false;//*/
        }

        public bool CheckTramCanBeStored(TramDTO tram, TrackDTO track)
        {
            if (!CheckTramType(tram, track))
            {
                return false;
            }
            foreach (SectorDTO sector in track.Sectors)
            {
                if (_sectorLogic.CheckIfSectorIsEmpty(sector)&&sector.SectorType != SectorType.Blocked)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckTramCanBeOverArched(TramDTO tram, TrackDTO overArchingTrack, TrackDTO overArchedTrack)
        {
            throw new NotImplementedException();
        }

        public void OverArchTram(TramDTO tram, TrackDTO overArchingTrack, TrackDTO overArchedTrack)
        {

        }

        public int AmountOfTramsOnTrack(TrackDTO track)
        {
            int AmountOfTrams = 0;

            foreach (SectorDTO sector in track.Sectors)
            {
                if (!_sectorLogic.CheckIfSectorIsEmpty(sector))
                {
                    AmountOfTrams++;
                }
            }

            return AmountOfTrams;
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
        public TrackDTO GetTrackByTramNumber(string number)
        {
            return _trackAccess.ReadTrackByTramNumber(number);
        }
    }
}
