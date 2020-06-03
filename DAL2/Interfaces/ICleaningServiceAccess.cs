using System;
using System.Collections.Generic;
using System.Text;
using DAL.Concrete;
using DTO;

namespace DAL.Interfaces
{
    public interface ICleaningServiceAccess : IGenAccess<CleaningServiceDTO>
    {
        void UpdateCleaningService(CleaningServiceDTO cleaningService);
        void StoreCleaningLog(CleaningLogDTO cleanLog);
        void UpdateCleanLog(CleaningLogDTO cleanLog);

        CleaningServiceDTO GetCleaningServiceByLocation(string Location);

        IEnumerable<CleaningLogDTO> GetCleaningLogs();
        IEnumerable<CleaningLogDTO> GetCleaningLogsByTramNumber(string tramnumber);
        
    }
}
