using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Logic
{
    public class Depot
    {
        public ICollection<UserDTO> Users { get; set; }
        private ICollection<TrackDTO> DepotTracks { get; set; }
        Track _tracklogic;
        
        public Depot(Track tracklogic)
        {
            this._tracklogic = tracklogic;
        }

        public void ReceiveTram(TramDTO tram, bool repairstatus, bool cleanstatus) 
        {

            if (CheckIfTramIsAllowed(tram))
            {
                //changeTramStatus(tram, repairstatus, cleanstatus);
                //AllocationManager.AllocateTramToService(tram);

                AllocationManager.AllocateTramToTrack(tram, DepotTracks, _tracklogic);
            }
        }

        private bool CheckIfTramIsAllowed(TramDTO tram)
        {
            throw new NotImplementedException();
        }

        private void changeTramStatus(TramDTO tram, bool repairstatus, bool cleanstatus)
        {
            if (repairstatus)
            {
                //add statusDTO to tram
            }
            if (cleanstatus)
            {
                //add statusDTO to tram
            }
        }

        public void testMapMethod(TramDTO tram)
        {
            //tramDTO -> logic tram class
        }
    }
}
