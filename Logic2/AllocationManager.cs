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


            /*
             * Nodig:
             * List repairtracks
             */

        public static void AllocateTramToTrack(TramDTO tram, List<TrackDTO> tracks, Track _trackLogic, Tram _tramLogic, RepairService _repairServiceLogic)
        {
            bool tramIsStored = false;
            if (TramNeedsToBeRepaired(tram, _tramLogic)) // gerepareerd worden
            {
               if ( checkIfSpotOnATrack(,_trackLogic, tram)) // check repairtracks
                {
                    _trackLogic.StoreTram(tram, MostEmptyTrack());
                    tramIsStored = true;
                }
                else
                {
                    _repairServiceLogic.AddTramToWaitingList(tram);
                }

            }
            if(!tramIsStored)// langparkeerplek
            {
                StatusDTO depotStatus = new StatusDTO();
                depotStatus.Status = TramStatus.Depot;
                if (tram.Status.Contains(depotStatus))
                {
                    // plaats tram op langparkeerplek
                }
            }
            if (!tramIsStored) // typegebonden
            {
                List<TrackDTO> typeTracks = new List<TrackDTO>();
                foreach (TrackDTO track in tracks)
                {

                    if (tram.Type == track.TramType && _trackLogic.CheckTramCanBeStored(tram, track))
                    {
                        typeTracks.Add(track);
                    }
                }

                if (typeTracks.Count != 0)
                {
                    _trackLogic.StoreTram(tram, MostEmptyTrack(typeTracks, _trackLogic, tram));
                    tramIsStored = true;
                    
                }
            }
            

            if(!tramIsStored) //lijngebonden
            {
                List<TrackDTO> lineTracks = new List<TrackDTO>();
                foreach (TrackDTO track in tracks)
                {
                    if (/*tram line*/ == track.PreferredTrackLine && _trackLogic.CheckTramCanBeStored(tram, track))
                    {
                        lineTracks.Add(track);
                    }
                }
                if (lineTracks.Count != 0)
                {
                    _trackLogic.StoreTram(tram, MostEmptyTrack(lineTracks, _trackLogic, tram));
                    tramIsStored = true;
                }
                else
                {
                    //overkoepelen?
                }
            }

            if (!tramIsStored) // normale spoor plek?
            {
                foreach (TrackDTO track in tracks)
                {
                    if (track.GetType == null && track.PreferredTrackLine == /*null?*/)
                    {

                    }
                }
            }
            if (!tramIsStored) //echt nergens plek?
            {
                foreach (TrackDTO track in tracks)
                {
                    if (_trackLogic.CheckTramCanBeStored(tram, track))
                    {
                        _trackLogic.StoreTram(tram, track);
                    }
                }
            }
            if (!tramIsStored) // plek op in/uitrit spoor
            {

            }
            if (!tramIsStored) // nergens plek
            {
                //stuur tram weg
            }
        }

        private static TrackDTO MostEmptyTrack(List<TrackDTO> tracks, Track _trackLogic, TramDTO tram)
        {
            TrackDTO returnedTrack = null;

            foreach (TrackDTO track in tracks)
            {
                if (_trackLogic.CheckTramCanBeStored(tram, track))
                {
                    if (returnedTrack == null)
                    {
                        returnedTrack = track;
                    }
                    else if (_trackLogic.AmountOfTramsOnTrack(returnedTrack) > _trackLogic.AmountOfTramsOnTrack(track))
                    {
                        returnedTrack = track;
                    }
                }
            }

            return returnedTrack;
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

        private static bool TramNeedsToBeRepaired (TramDTO tram, Tram _tramlogic)
        {
            StatusDTO defectStatus = new StatusDTO();
            defectStatus.Status = TramStatus.Defect;
            if (tram.Status.Contains(defectStatus))
            {
                return true;
            }
            //if scheduled for repair, return true

            return false;
        }
    }
}
