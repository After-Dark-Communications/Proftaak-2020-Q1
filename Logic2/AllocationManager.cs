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
            if (tram.Status.Any(s => s.Status == TramStatus.Defect))
            {
                //add tram to repairservice
            }
            if (tram.Status.Any(s => s.Status == TramStatus.Cleaning))
            {
                //add tram to repairservice
            }
            // add tram to cleaningservice
        }

        public static void AllocateTramToTrack(TramDTO tram, ICollection<TrackDTO> tracks, Track _Tracklogic)
        {
            //if tram is in the repairservice -> send to a repair trakk


            foreach (TrackDTO track in tracks)
            {

                if (_Tracklogic.CheckTramCanBeStored(tram, track))
                {
                    _Tracklogic.StoreTram(tram, track);
                }
            }
           
            

            throw new NotImplementedException();
        }
    }
}
