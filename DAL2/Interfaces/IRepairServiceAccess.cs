using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL.Interfaces
{
    public interface IRepairServiceAccess
    {
        public void StoreRepairLog(RepairLogDTO repairLog);
        IEnumerable<RepairLogDTO> GetRepairLogs();
    }
}
