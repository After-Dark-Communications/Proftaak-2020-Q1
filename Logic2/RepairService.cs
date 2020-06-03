using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Transactions;
using Services;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class RepairService
    {
        private readonly IServiceAccess _serviceaccess;
        private readonly IRepairServiceAccess _repairServiceAccess;
        private readonly RepairServiceDTO _repairService;
        private readonly ITramAccess _tramAccess;

        public RepairService(IServiceAccess serviceaccess, IRepairServiceAccess repairServiceAccess, ITramAccess tramAccess)
        {
            _serviceaccess = serviceaccess;
            _repairServiceAccess = repairServiceAccess;
            _tramAccess = tramAccess;
            _repairService = GetService();
            DetermineIfRepairNeedToBeReset();
            
        }
        // repairdate, occured, userID, repairID
        public void ServiceRepair(TramDTO tram, UserDTO user)
        {
            if(CanRepairTram(_repairService, tram))
            {
                _tramAccess.DeleteStatus(TramStatus.Defect, tram);
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Defect);
                RepairLogDTO repair = _repairServiceAccess.GetRepairLogsByTramNumber(tram.TramNumber).SingleOrDefault(x => x.Occured == false);
                removeServiceCounter(repair);
                repair.RepairDate = DateTime.Now;
                repair.Occured = true;
                repair.User = user;
                UpdateLog(repair);
            }
        }

        public void removeServiceCounter(RepairLogDTO repair)
        {
            RepairServiceDTO repairService = _repairServiceAccess.GetRepairServiceByLocation("RMS");
            if (repair.ServiceType == ServiceType.Big)
            {
                repairService.MaxBigServicePerDay--;
            }
            else
            {
                repairService.MaxSmallServicePerDay--;
            }
            _repairServiceAccess.UpdateRepairService(repairService);
                
        }

        public void DetermineRepairType(TramDTO tram)
        {
            RepairLogDTO repairLogDTOBig = GetOccuredLog(tram, ServiceType.Big);
            RepairLogDTO repairLogDTOSmall = GetOccuredLog(tram, ServiceType.Small);
            if (repairLogDTOBig == null)
            {
                repairLogDTOBig = new RepairLogDTO();
                repairLogDTOBig.RepairDate = default;
            }
            if (repairLogDTOSmall == null)
            {
                repairLogDTOSmall = new RepairLogDTO();
                repairLogDTOSmall.RepairDate = default;
            }    
           
            DateTime startTimeBig = repairLogDTOBig.RepairDate;
            DateTime startTimeSmall = repairLogDTOSmall.RepairDate;
            DateTime endTime = DateTime.Now;

            TimeSpan spanBig = endTime.Subtract(startTimeBig);
            TimeSpan spanSmall = endTime.Subtract(startTimeSmall);

            if (spanBig.TotalDays > 183 || repairLogDTOBig.RepairDate == default)
            {
                tram.OccuredRepairLog = repairLogDTOBig;
                CreateRepairLogScheduled(tram, ServiceType.Big);
            }
            else if(spanSmall.TotalDays > 91 || repairLogDTOSmall.RepairDate == default) 
            {
                tram.OccuredRepairLog = repairLogDTOSmall;
                CreateRepairLogScheduled(tram, ServiceType.Small);
            }
        }

        public void CreateRepairLogDefect(TramDTO tram, string repairMessage)
        {
            RepairLogDTO log = new RepairLogDTO(_repairServiceAccess.GetRepairServiceByLocation("RMS"), tram, ServiceType.Big, false, false, repairMessage);
            _repairServiceAccess.StoreRepairLog(log);
        }
        public void CreateRepairLogScheduled(TramDTO tram, ServiceType service)
        {
            RepairLogDTO log = new RepairLogDTO(_repairServiceAccess.GetRepairServiceByLocation("RMS"), tram, service, false, false, "Scheduled");
            _repairServiceAccess.StoreRepairLog(log);
        }
        private bool CanRepairTram(RepairServiceDTO Service, TramDTO tram)
        {
            if (Service.MaxBigServicePerDay == 0 && Service.MaxSmallServicePerDay == 0)
            {
                return false;
            }
            return true;
        }
        private RepairServiceDTO GetService()
        {
            return _repairServiceAccess.GetRepairServiceByLocation("RMS");
        }
        private void UpdateLog(RepairLogDTO repair)
        {
            _repairServiceAccess.UpdateRepairLog(repair);
        }
        private void ResetRepair()
        {
            _repairService.MaxBigServicePerDay = 1;
            _repairService.MaxSmallServicePerDay = 4;
        }
        private void DetermineIfRepairNeedToBeReset()
        {
            DateTime CurrentDate = DateTime.Now;
            DateTime LastRepair = GetRepairHistory().Max(x => x.RepairDate.Date);
            TimeSpan span = CurrentDate.Subtract(LastRepair);
            if(span.TotalHours > 24)
            {
                ResetRepair();
            }
        }

        public void RepairTram(RepairLogDTO repairLog)
        {
            _repairServiceAccess.StoreRepairLog(repairLog);
        }

        public IEnumerable<RepairLogDTO> GetRepairHistory()
        {
            return _repairServiceAccess.GetRepairLogs();
        }
        public RepairLogDTO GetOccuredLog(TramDTO tram, ServiceType serviceType)
        {
            RepairLogDTO Occured = _repairServiceAccess.GetRepairLogsByTramNumber(tram.TramNumber).Where(x => x.Occured == true).Where(x => x.ServiceType == serviceType).OrderBy(x => x.RepairDate).FirstOrDefault();
            return Occured;
        }
        public RepairLogDTO GetNotOccuredLog(TramDTO tram)
        {
            RepairLogDTO NotOccured = _repairServiceAccess.GetRepairLogsByTramNumber(tram.TramNumber).SingleOrDefault(x => x.Occured == false);
            return NotOccured;
        }
        public bool CheckNotOccuredLog(TramDTO tram)
        {
            bool NotOccured = _repairServiceAccess.GetRepairLogsByTramNumber(tram.TramNumber).Any(x => x.Occured == false);
            return NotOccured;
        }

        public void AddTramToWaitingList(TramDTO tram)
        {
            RepairLogDTO WaitingList = _repairServiceAccess.GetRepairLogsByTramNumber(tram.TramNumber).SingleOrDefault(x => x.Occured == false);
            WaitingList.WaitingList = true;
            _repairServiceAccess.UpdateWaitingList(WaitingList);
        }
    }
}

