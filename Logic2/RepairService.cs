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

        public void AddTramToWaitingList(TramDTO tram)
        {
            throw new NotImplementedException();
        }

        public void RepairTram(Tram tram)
        {
            throw new NotImplementedException();
        }

    }
}
