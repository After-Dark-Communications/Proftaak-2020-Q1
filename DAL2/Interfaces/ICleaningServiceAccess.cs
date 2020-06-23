using System;
using System.Collections.Generic;
using System.Text;
using DAL.Concrete;
using DTO;

namespace DAL.Interfaces
{
    public interface ICleaningServiceAccess 
    {
        void UpdateCleaningService(CleaningServiceDTO cleaningService);
        void StoreCleaningLog(CleaningLogDTO cleanLog);
        void UpdateCleanLog(CleaningLogDTO cleanLog);
        void UpdateSchedulingCleanLog(CleaningLogDTO cleanLog);
        void DeleteNotOccured();

        CleaningServiceDTO GetCleaningServiceByLocation(string Location);

        IEnumerable<CleaningLogDTO> GetCleaningLogs();
        IEnumerable<CleaningLogDTO> GetCleaningLogsByTramNumber(string tramnumber);
        
    }
}
