using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Linq;
using Services;

namespace Logic
{
    public static class AllocationManager
    {

        public static void AllocateTramToService(TramDTO tram)
        {
            // StatusDTO status = cleaning
            tram.Status.Any(s => s.Status == TramStatus.Defect);
               // throw new NotImplementedException();
        }

        public static void AllocateTramToTrack(TramDTO tram, ICollection<TrackDTO> tracks, Track _Tracklogic)
        {
            
            foreach (TrackDTO track in tracks)
            {

                if (_Tracklogic.CheckTramType(tram, track));
            }
           
            

            throw new NotImplementedException();
        }
    }
}
