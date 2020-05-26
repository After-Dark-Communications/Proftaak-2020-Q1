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
         * waiting list //voor later
         * bij welke lijn hoort een tram 
         * tram weer weg kunnen sturen
         */


        private static bool storeTram(IEnumerable<TrackDTO> tracks, TramDTO tram, Track _trackLogic)
        {
            if (tracks.Count() > 0)
            {
                _trackLogic.StoreTram(tram, MostEmptyTrack(tracks, _trackLogic, tram));
                return true;
            }
            return false;
        }

        public static void AllocateTramToTrack(TramDTO tram, List<TrackDTO> tracks, Track _trackLogic, Tram _tramLogic, RepairService _repairServiceLogic)
        {
            bool tramIsStored = false;
            if (TramNeedsToBeRepaired(tram, _tramLogic)) // gerepareerd worden
            {
                IEnumerable<TrackDTO> repairTracks = tracks.Where(t => t.Type == TrackType.Repair && _trackLogic.CheckTramCanBeStored(tram, t));
                tramIsStored = storeTram(repairTracks, tram, _trackLogic);
                if (!tramIsStored)
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
                    IEnumerable<TrackDTO> depotTracks = tracks.Where(t => t.Type == TrackType.LongParking && _trackLogic.CheckTramCanBeStored(tram, t));
                    tramIsStored = storeTram(depotTracks, tram, _trackLogic);
                }
            }
            if (!tramIsStored) // typegebonden
            {
                IEnumerable<TrackDTO> typeTracks = tracks.Where(t => t.TramType == tram.Type && _trackLogic.CheckTramCanBeStored(tram, t));
                tramIsStored = storeTram(typeTracks, tram, _trackLogic);
            }
           
            if(!tramIsStored) //lijngebonden
            {

                IEnumerable<TrackDTO> lineTracks = tracks.Where(t => t.PreferredTrackLine == tram.Line && _trackLogic.CheckTramCanBeStored(tram, t));
                tramIsStored = storeTram(lineTracks, tram, _trackLogic);
                
                if (!tramIsStored)
                {
                    foreach (TrackDTO track in lineTracks)
                    {

                        //_trackLogic.CheckTramCanBeOverArched(tram, track)
                    }
                     
                }
            }

            if (!tramIsStored) // normale spoor plek?
            {
                foreach (TrackDTO track in tracks)
                {

                }
                IEnumerable<TrackDTO> normalTracks = tracks.Where(t => t.Type == TrackType.Normal && _trackLogic.CheckTramCanBeStored(tram, t));

                    tramIsStored = storeTram(normalTracks, tram, _trackLogic);
                
            }
            if (!tramIsStored) // plek op in/uitrit spoor
            {
                IEnumerable<TrackDTO> entranceExitTracks = tracks.Where(t => t.Type == TrackType.EntranceExit && _trackLogic.CheckTramCanBeStored(tram, t));
              
                    tramIsStored = storeTram(entranceExitTracks, tram, _trackLogic);
                
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

        private static TrackDTO MostEmptyTrack(IEnumerable<TrackDTO> tracks, Track _trackLogic, TramDTO tram)
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

        private static bool checkIfSpotOnATrack(IEnumerable<TrackDTO> tracks, Track _tracklogic, TramDTO tram)
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
            if (tram.Status.Any(x => x.Status == TramStatus.Defect))
            {
                return true;
            }
            // (if tram has repair log that has not yet occurred. return true)
            return false;
        }
    }
}
