using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL.Interfaces
{
    public interface IRepairServiceAcces
    {
        public void StoreRepairLog(RepairLogDTO repairLog);
    }
}
