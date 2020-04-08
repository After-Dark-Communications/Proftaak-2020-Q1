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
            throw new NotImplementedException();
        }

        public void ReceiveTram(Tram tram, bool defect, bool cleaning)
        {
            //kijken of de tram bij onze remise naar binnen mag komen
            if (CheckIfTramIsAllowed(tram))
            {
                changeTramStatus(tram, defect, cleaning);
                AllocationManager.AllocateTramToService(tram);
                AllocationManager.AllocateTramToTrack(tram);
            }
            else
            {
                //tram terugsturen / weigeren
            }
        }

        private void changeTramStatus(Tram tram, bool defect, bool cleaning)
        {
            if (defect)
            {
                tram.ChangeStatus(TramStatus.Defect);
            }
            if (cleaning)
            {
                tram.ChangeStatus(TramStatus.Cleaning);
            }
        }
    }
}
