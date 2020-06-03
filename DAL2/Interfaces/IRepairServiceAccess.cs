using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using Services;

namespace DAL.Interfaces
{
    public interface IRepairServiceAccess
    {
        void UpdateRepairService(RepairServiceDTO repairService);
        void StoreRepairLog(RepairLogDTO repairLog);
        void UpdateRepairLog(RepairLogDTO repairLog);
        void UpdateWaitingList(RepairLogDTO repairLog);

        RepairServiceDTO GetRepairServiceByLocation(string Location);

        IEnumerable<RepairLogDTO> GetRepairLogs();
        IEnumerable<RepairLogDTO> GetRepairLogsByTramNumber(string tramnumber);
        
    }
}
