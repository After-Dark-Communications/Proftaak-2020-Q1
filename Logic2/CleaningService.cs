using DAL.Concrete;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return true;
        }
        private CleaningServiceDTO GetService()
        {
            return _serviceaccess.ReadCleaning();
        }
        private void ResetCleaning()
        {
            _cleaningServiceDTO.MaxBigServicePerDay = 2;
            _cleaningServiceDTO.MaxSmallServicePerDay = 3;
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

        public void CleanTram(TramDTO tram)
        {
            if (CanCleanTram(_cleaningServiceDTO))
            {
                tram.Status.RemoveAll(cleaning => cleaning.Status == Services.TramStatus.Cleaning);
                _tramAccess.DeleteStatus(TramStatus.Cleaning, tram);
                CleaningLogDTO cleaningLog = _cleaningAccess.GetCleaningLogsByTramNumber(tram.TramNumber).Single(x => x.Occured == false);
                removeServiceCounter(cleaningLog);
                cleaningLog.Occured = true;
                UpdateLog(cleaningLog);
            }
        }

        public void removeServiceCounter(CleaningLogDTO cleaningLog)
        {
            CleaningServiceDTO cleaningService = _cleaningAccess.GetCleaningServiceByLocation("RMS");
            if (cleaningLog.ServiceType == ServiceType.Big)
            {
                cleaningService.MaxBigServicePerDay--;
            }
            else
            {
                cleaningService.MaxSmallServicePerDay--;
            }
            _cleaningAccess.UpdateCleaningService(cleaningService);
        }

        private void UpdateLog(CleaningLogDTO cleaning)
        {
            _cleaningAccess.UpdateCleanLog(cleaning);
        }

        public void CreateCleaningLog(TramDTO tram)
        {
            CleaningLogDTO cleaningLog = new CleaningLogDTO(_cleaningAccess.GetCleaningServiceByLocation("RMS"), tram, ServiceType.Big);

        }

        public void DetermineCleaningType(TramDTO tram)
        {
            CleaningLogDTO CleaningLogDTOBig = GetOccuredLog(tram, ServiceType.Big);
            CleaningLogDTO  cleaningLogDTOSmall = GetOccuredLog(tram, ServiceType.Small);
            if (CleaningLogDTOBig == null)
            {
                CleaningLogDTOBig = new CleaningLogDTO();
                CleaningLogDTOBig.Date = default;
            }
            if (cleaningLogDTOSmall == null)
            {
                cleaningLogDTOSmall = new CleaningLogDTO();
                cleaningLogDTOSmall.Date = default;
            }

            DateTime startTimeBig = CleaningLogDTOBig.Date;
            DateTime startTimeSmall = cleaningLogDTOSmall.Date;
            DateTime endTime = DateTime.Now;

            TimeSpan spanBig = endTime.Subtract(startTimeBig);
            TimeSpan spanSmall = endTime.Subtract(startTimeSmall);

            if (spanBig.TotalDays > 183 || CleaningLogDTOBig.Date == default)
            {
                tram.OccuredCleanLog = CleaningLogDTOBig;
                CreateCleaningLogScheduled(tram, ServiceType.Big);
            }
            else if (spanSmall.TotalDays > 91 || cleaningLogDTOSmall.Date == default)
            {
                tram.OccuredCleanLog = cleaningLogDTOSmall;
                CreateCleaningLogScheduled(tram, ServiceType.Small);
            }
        }

        public CleaningLogDTO GetOccuredLog(TramDTO tram, ServiceType serviceType)
        {
            CleaningLogDTO Occured = _cleaningAccess.GetCleaningLogsByTramNumber(tram.TramNumber).Where(x => x.Occured).Where(x => x.ServiceType == serviceType).OrderBy(x => x.Date).FirstOrDefault();
            return Occured;
        }

        public void CreateCleaningLogScheduled(TramDTO tram, ServiceType service)
        {
            CleaningLogDTO log = new CleaningLogDTO(_cleaningAccess.GetCleaningServiceByLocation("RMS"), tram, service, false);
            _cleaningAccess.StoreCleaningLog(log);
        }
        public void SetManualDate(DateTime date, TramDTO tram, UserDTO user)
        {          
            if(CanCleanTram(_cleaningServiceDTO))
            {
                DateTime setDate = DateTime.Now;
                tram.CleaningDateBigService = date;
                CleaningLogDTO log = new CleaningLogDTO(_cleaningAccess.GetCleaningServiceByLocation("RMS"), tram, user, setDate, ServiceType.Big, false);
                _cleaningAccess.StoreCleaningLog(log);
            }
            else
            {
                ResetCleaning();
            }
        }
    }
}
