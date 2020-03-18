using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.Concrete;

namespace Logic
{
    public class Depot
    {
        public ICollection<User> Users { get; set; }
        private ICollection<Track> DepotTracks { get; set; }

        private bool CheckIfTramIsAllowed(Tram tram)
        {
            //check if tram number is known 
            if (tram == )
            {

            }
            throw new NotImplementedException();
        }

        public void ReceiveTram(Tram tram) 
        {
            if (CheckIfTramIsAllowed(tram))
            {
                AllocationManager.AllocateTramToTrack(tram);
            }
        }
    }
}
