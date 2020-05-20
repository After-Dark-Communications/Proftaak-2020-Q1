using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
<<<<<<< HEAD
using DTO;

=======
using System.Transactions;
>>>>>>> BleedingEdge

namespace Logic
{
    public class RepairService
    {
        private readonly IServiceAccess _serviceaccess;
        public readonly IRepairServiceAccess _repairServiceAccess;
        private readonly RepairServiceDTO _repairService;

        public RepairService(IServiceAccess serviceaccess, IRepairServiceAccess repairServiceAccess)
        {
            _serviceaccess = serviceaccess;
            _repairServiceAccess = repairServiceAccess;
            _repairService = GetService();
            DetermineIfRepairNeedToBeReset();
            
        }
        public void SetSmallRepairTram(TramDTO tram)
        {
            if(CanRepairTram(_repairService))
            {
                DateTime RepairDate = DateTime.Now;
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Defect);
                tram.RepairDateSmallService = RepairDate;
                _repairService.MaxSmallServicePerDay--;
            }
        }
        public void SetLargeRepairTram(TramDTO tram)
        {
            if(CanRepairTram(_repairService))
            {
                DateTime RepairDate = DateTime.Now;
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Defect);
                tram.RepairDateBigService = RepairDate;
                _repairService.MaxBigServicePerDay--;
            }
        }
        public void DetermineRepairType(TramDTO tram)
        {
            DateTime startTimeBig = tram.RepairDateBigService;
            DateTime startTimeSmall = tram.RepairDateSmallService;
            DateTime endTime = DateTime.Now;

            TimeSpan spanBig = endTime.Subtract(startTimeBig);
            TimeSpan spanSmall = endTime.Subtract(startTimeSmall);


            if(spanBig.TotalDays > 183)
            {
                SetLargeRepairTram(tram);
            }
            else if(spanSmall.TotalDays > 91)
            {
                SetSmallRepairTram(tram);
            }
        }
        private bool CanRepairTram(RepairServiceDTO Service)
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
        private RepairServiceDTO GetService()
        {
            return _serviceaccess.ReadRepair();
        }
        private void ResetRepair()
        {
            _repairService.MaxBigServicePerDay = 1;
            _repairService.MaxSmallServicePerDay = 4;
        }
        private void DetermineIfRepairNeedToBeReset()
        {
            DateTime CurrentDate = DateTime.Now;
            DateTime LastRepair = GetService().RepairDate;
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
    }
}

