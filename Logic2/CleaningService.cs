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
        private readonly CleaningServiceDTO _cleaningServiceDto;
        private readonly ICleaningServiceAccess _cleaningAccess;
        private readonly ITramAccess _tramAccess;
        private readonly IUserAccess _userAccess;

        public CleaningService(IServiceAccess serviceaccess, ICleaningServiceAccess cleaningAccess, ITramAccess tramAccess, IUserAccess access)
        {
            _serviceaccess = serviceaccess;
            _cleaningAccess = cleaningAccess;
            _tramAccess = tramAccess;
            _cleaningServiceDto = GetService();
            _userAccess = access;
        }
        public void ServiceRepair(TramDTO tram, UserDTO user)
        {
            if (CanCleanTram(_cleaningServiceDto))
            {
                _tramAccess.DeleteStatus(TramStatus.Defect, tram);
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Defect);
                CleaningLogDTO cleaningLog = _cleaningAccess.GetCleaningLogsByTramNumber(tram.TramNumber).SingleOrDefault(x => x.Occured == false);
                RemoveServiceCounter(cleaningLog);
                cleaningLog.CleaningDate = DateTime.Now;
                cleaningLog.Occured = true;
                cleaningLog.User = user;
                UpdateLog(cleaningLog);

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
            return _cleaningAccess.GetCleaningServiceByLocation("RMS");
        }
        private void ResetCleaning()
        {
            _cleaningServiceDto.MaxBigServicePerDay = 2;
            _cleaningServiceDto.MaxSmallServicePerDay = 3;
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
            if (CanCleanTram(_cleaningServiceDto))
            {
                tram.Status.RemoveAll(cleaning => cleaning.Status == Services.TramStatus.Cleaning);
                _tramAccess.DeleteStatus(TramStatus.Cleaning, tram);
                CleaningLogDTO cleaningLog = _cleaningAccess.GetCleaningLogsByTramNumber(tram.TramNumber).Single(x => x.Occured == false);
                RemoveServiceCounter(cleaningLog);
                cleaningLog.Occured = true;
                UpdateLog(cleaningLog);
            }
        }

        public void RemoveServiceCounter(CleaningLogDTO cleaningLog)
        {
            CleaningServiceDTO cleaningService = _cleaningAccess.GetCleaningServiceByLocation("RMS");
            if (cleaningLog.CleaningType == ServiceType.Big)
            {
                cleaningService.MaxBigServicePerDay--;
                cleaningService.MaxSmallServicePerDay--;
            }
            if (cleaningLog.CleaningType == ServiceType.Small)
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
        public DateTime DetermineCleaningDate(CleaningLogDTO clog)
        {
            DateTime currentTime = DateTime.Now;
            DateTime RepairTime = DateTime.Today;
            TimeSpan difference = clog.CleaningDate - currentTime;
            if(clog.CleaningDate == null)
            {
                return RepairTime;
            }
            if (difference.TotalDays >= 2 || CanCleanTram(_cleaningServiceDto))
            {
                RepairTime = currentTime.AddDays(1);
                return RepairTime;
            }
            return RepairTime;
        }

        public void DetermineCleaningType(TramDTO tram)
        {
            CleaningLogDTO CleaningLogDTOBig = GetOccuredLog(tram, ServiceType.Big);
            CleaningLogDTO  cleaningLogDTOSmall = GetOccuredLog(tram, ServiceType.Small);
            if (CleaningLogDTOBig == null)
            {
                CleaningLogDTOBig = new CleaningLogDTO();
                CleaningLogDTOBig.CleaningDate = default;
            }
            if (cleaningLogDTOSmall == null)
            {
                cleaningLogDTOSmall = new CleaningLogDTO();
                cleaningLogDTOSmall.CleaningDate = default;
            }

            DateTime startTimeBig = CleaningLogDTOBig.CleaningDate;
            DateTime startTimeSmall = cleaningLogDTOSmall.CleaningDate;
            DateTime endTime = DateTime.Now;

            TimeSpan spanBig = endTime.Subtract(startTimeBig);
            TimeSpan spanSmall = endTime.Subtract(startTimeSmall);

            if (spanBig.TotalDays > 183 || CleaningLogDTOBig.CleaningDate == default)
            {
                tram.OccuredCleanLog = CleaningLogDTOBig;
                CreateCleaningLogScheduled(tram, ServiceType.Big);
            }
            else if (spanSmall.TotalDays > 91 || cleaningLogDTOSmall.CleaningDate == default)
            {
                tram.OccuredCleanLog = cleaningLogDTOSmall;
                CreateCleaningLogScheduled(tram, ServiceType.Small);
            }
        }

        public CleaningLogDTO GetOccuredLog(TramDTO tram, ServiceType serviceType)
        {
            CleaningLogDTO Occured = _cleaningAccess.GetCleaningLogsByTramNumber(tram.TramNumber).Where(x => x.Occured).Where(x => x.CleaningType == serviceType).OrderBy(x => x.CleaningDate).FirstOrDefault();
            return Occured;
        }

        public void CreateCleaningLogScheduled(TramDTO tram, ServiceType service)
        {
            CleaningLogDTO log = new CleaningLogDTO(_cleaningAccess.GetCleaningServiceByLocation("RMS"), tram, service, false);
            _cleaningAccess.StoreCleaningLog(log);
        }

        public void HasToBeCleaned(TramDTO tram, ServiceType serviceType)
        {
            CleaningLogDTO cleaningLog = new CleaningLogDTO(_cleaningAccess.GetCleaningServiceByLocation("RMS"), tram, serviceType);
            _cleaningAccess.StoreCleaningLog(cleaningLog);
        }

        public CleaningLogDTO GetNotOccuredLog(TramDTO tram)
        {
            CleaningLogDTO NotOccured = _cleaningAccess.GetCleaningLogsByTramNumber(tram.TramNumber).SingleOrDefault(x => x.Occured == false);
            return NotOccured;
        }

        public void DeleteNotOccured()
        {
            _cleaningAccess.DeleteNotOccured(false);
        }
        private bool CanCleanTram(CleaningServiceDTO clean, TramDTO tram)
        {
            if (clean.MaxBigServicePerDay == 0 && clean.MaxSmallServicePerDay == 0)
            {
                return false;
            }
            return true;
        }
        public void AssignUser(string username, TramDTO tram)
        {
            UserDTO user = _userAccess.GetUserByUsername(username);
            CleaningLogDTO NotOccured = GetNotOccuredLog(tram);
            NotOccured.User = user;
            NotOccured.CleaningDate = DateTime.Today;
            _cleaningAccess.UpdateAssignUser(NotOccured);
        }
    }
}
