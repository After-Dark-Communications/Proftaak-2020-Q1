using System;
using System.Collections.Generic;
using System.Text;
using DTO;


namespace Logic
{
    public class RepairService : Service
    {
        public override int MaxBigServicePerDay { get; set; }
        public override int MaxSmallServicePerDay { get; set; }
        public ICollection<Track> AllocatedTracks { get; set; }
        private Queue<TramDTO> waitingList;

        public void AddTramToWaitingList(TramDTO tram)
        {
            waitingList.Enqueue(tram);
            // ipv dit iets met database? anders bij herstart programma is de list leeg
        }

        public void RepairTram(Tram tram)
        {
            throw new NotImplementedException();
        }

    }
}
