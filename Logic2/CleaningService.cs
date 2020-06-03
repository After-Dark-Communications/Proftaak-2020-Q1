using DAL.Concrete;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Services;


namespace Logic
{
    public class CleaningService
    {
        private readonly IServiceAccess _serviceaccess;
        private readonly CleaningServiceDTO _cleaningServiceDTO;
        private readonly ICleaningServiceAccess _cleaningAccess;
        private readonly ITramAccess _tramAccess;

        public CleaningService(IServiceAccess serviceaccess, ICleaningServiceAccess cleaningAccess, ITramAccess tramAccess)
        {
            _serviceaccess = serviceaccess;
            _cleaningServiceDTO = GetService();
            _cleaningAccess = cleaningAccess;
            _tramAccess = tramAccess;
        }
        public void SetSmallCleanTram(TramDTO tram)
        {
            if (CanCleanTram(_cleaningServiceDTO))
            {
                DateTime RepairDate = DateTime.Now;
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Cleaning);
                tram.RepairDateSmallService = RepairDate;
                _cleaningServiceDTO.MaxSmallServicePerDay--;
            }
        }
        public void SetLargeRepairTram(TramDTO tram)
        {
            if (CanCleanTram(_cleaningServiceDTO))
            {
                DateTime RepairDate = DateTime.Now;
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Cleaning);
                tram.RepairDateBigService = RepairDate;
                _cleaningServiceDTO.MaxBigServicePerDay--;
            }
        }
        private bool CanCleanTram(CleaningServiceDTO Service)
        {
            if (Service.MaxBigServicePerDay == 0 && Service.MaxSmallServicePerDay == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private CleaningServiceDTO GetService()
        {
            return _serviceaccess.ReadCleaning();
        }
        private void ResetCleaning()
        {
            _cleaningServiceDTO.MaxBigServicePerDay = 4;
            _cleaningServiceDTO.MaxSmallServicePerDay = 2;
        }
        private void DetermineIfCleanNeedToBeReset()
        {
            DateTime CurrentDate = DateTime.Now;
            DateTime LastClean = GetService().CleanDate;
            TimeSpan span = CurrentDate.Subtract(LastClean);
            if (span.TotalHours > 24)
            {
                ResetCleaning();
            }
        }
        public IEnumerable<CleaningLogDTO> GetCleaningLogs()
        {
            return _cleaningAccess.GetCleaningLogs();
        }

        public void RemoveCleaningStatus(TramDTO tram)
        {
            _tramAccess.DeleteStatus(TramStatus.Cleaning, tram);
        }
        public void SetManualDate(DateTime date, TramDTO tram)
        {          
            if(CanCleanTram(_cleaningServiceDTO))
            {
                tram.CleaningDateBigService = date;
            }
            else
            {
                ResetCleaning();
            }
        }
    }
}
