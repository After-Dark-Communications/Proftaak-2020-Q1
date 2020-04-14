using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Logic
{
    public class Track
    {
        //allowed tramtypes
        private bool CheckTramType(TramDTO tram, TrackDTO track)
        {
            
            //TrackDTO toegestane types
             //   tramDTO type
            // if allowed types is empty, or the type exists in the allowed types
            throw new NotImplementedException();
        }

        public bool CheckTramCanBeStored(TramDTO tram, TrackDTO track)
        {
            CheckTramType(tram, track);
            //check if there is still a spot in a sector
            throw new NotImplementedException();
        }

        public void StoreTram(TramDTO tram)
        {
            //
            throw new NotImplementedException();
        }


    }
}
