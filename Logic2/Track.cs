using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Logic
{
    public class Track
    {
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

            throw new NotImplementedException();
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


    }
}
