using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using Services;

namespace Logic
{
    public class Depot
    {
        public ICollection<UserDTO> Users { get; set; }
        private ICollection<TrackDTO> DepotTracks { get; set; }
        Track _tracklogic;
        Tram _tramlogic;

        public Depot(Track tracklogic, Tram tramlogic)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
        }

        public void ReceiveTram(int tramNumber, bool repairstatus, bool cleanstatus) 
        {
            TramDTO tram = _tramlogic.GetTram(tramNumber);

            if (CheckIfTramIsAllowed(tram))
            {
                changeTramStatus(tram, repairstatus, cleanstatus, _tramlogic);
                AllocationManager.AllocateTramToService(tram);
                AllocationManager.AllocateTramToTrack(tram, DepotTracks, _tracklogic);
            }
        }

        private bool CheckIfTramIsAllowed(TramDTO tram)
        {
            throw new NotImplementedException();
        }

        private void changeTramStatus(TramDTO tram, bool repairstatus, bool cleanstatus, Tram _tramlogic)
        {
            if (repairstatus)
            {
                //hoe ga ik een statusDTO meegeven?
               //tramlogic.AddStatus(., tram);
            }
            if (cleanstatus)
            {
                //add statusDTO to tram
            }
        }

    }
}
