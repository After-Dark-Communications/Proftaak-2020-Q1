using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;


namespace Logic
{
    public class RepairService : Service
    {
        public override int MaxBigServicePerDay { get; set; }
        public override int MaxSmallServicePerDay { get; set; }
        public ICollection<Track> AllocatedTracks { get; set; }

        public IRepairServiceAcces RepairServiceAcces { get; private set; }

        public RepairService(IRepairServiceAcces repairServiceAcces)
        {
            RepairServiceAcces = repairServiceAcces;
        }

        public void RepairTram(RepairLogDTO repairLog)
        {
            RepairServiceAcces.StoreRepairLog(repairLog);
        }

    }
}
