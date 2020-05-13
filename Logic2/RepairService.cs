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

        public IRepairServiceAccess RepairServiceAccess { get; private set; }

        public RepairService(IRepairServiceAccess repairServiceAccess)
        {
            RepairServiceAccess = repairServiceAccess;
        }

        public void RepairTram(RepairLogDTO repairLog)
        {
            RepairServiceAccess.StoreRepairLog(repairLog);
        }

    }
}
