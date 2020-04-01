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

        private bool CheckIfTramIsAllowed(TramDTO tram)
        {
            throw new NotImplementedException();
        }

        public void ReceiveTram(TramDTO tram) 
        {
            if (CheckIfTramIsAllowed(tram))
            {
                AllocationManager.AllocateTramToTrack(tram);
            }
        }
    }
}
