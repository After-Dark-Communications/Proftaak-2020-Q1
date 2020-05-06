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
            if (TramNeedsToBeRepaired())
            {
                if (CheckIfSpotOnATrack(_repairlogic.fetchAllTracksFromRepairservice(repairserviceDTO), _Tracklogic, tram)) ;
                {
                    //add tram to track
                    return true;
                }
                else
                {
                    // voeg tram toe aan wachtlijst
                    return false;
                }

            }else if()
            {

            }


        }


        private static bool checkIfSpotOnATrack(List<TrackDTO> tracks, Track _tracklogic, TramDTO tram)
        {
            foreach (TrackDTO track in tracks)
            {
                if (_tracklogic.CheckTramCanBeStored(tram, track))
                {
                    return true;
                }
            }
            return false;
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

        private static bool TramNeedsToBeRepaired ()
        {
            throw new NotImplementedException();
        }
    }
}
