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
        
        //public static void AllocateTramToService(TramDTO tram, RepairService _repairServiceLogic, CleaningService _cleaningServiceLogic)
        //{
        //    if (tram.Status.Any(s => s.Status == TramStatus.Defect))
        //    {
        //        _repairServiceLogic.AddTram(tram);
        //    }
        //    if (tram.Status.Any(s => s.Status == TramStatus.Cleaning))
        //    {
        //        _cleaningServiceLogic.AddTram(tram);
        //    }
        //}

        public static void AllocateTramToTrack(TramDTO tram, List<TrackDTO> tracks, Track _Tracklogic)
        {
            //if tram is in the repairservice -> send to a repair track

            foreach (TrackDTO track in tracks)
            {
                if (_Tracklogic.CheckTramCanBeStored(tram, track))
                {
                    _Tracklogic.StoreTram(tram, track);
                    break;
                }
            }

        }
        public static void AllocateToRandomTrack(TramDTO tram, List<TrackDTO> tracks, Track _Tracklogic)
        {
            bool tramIsStored = false;
            do
            {
                TrackDTO randomTrack = _Tracklogic.GetRandomTrack(tracks);
                if (_Tracklogic.CheckTramCanBeStored(tram, randomTrack))
                {
                    _Tracklogic.StoreTram(tram, randomTrack);
                    tramIsStored = true;
                }
            } while (tramIsStored == false);
        }
    }
}
