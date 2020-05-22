using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL.Interfaces
{
    public interface IRepairServiceAccess
    {
        public void UpdateRepairService(RepairServiceDTO repairService);
        public void StoreRepairLog(RepairLogDTO repairLog);
        IEnumerable<RepairLogDTO> GetRepairLogs();
        public void UpdateRepairLog(RepairLogDTO repairLog);
        public void UpdateWaitingList(RepairLogDTO repairLog);
        public IEnumerable<RepairLogDTO> GetRepairLogsByTramNumber(string tramnumber);
        public RepairServiceDTO GetRepairServiceByLocation(string Location);


    }
}
