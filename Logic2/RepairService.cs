using DAL.Concrete;
using DTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Transactions;

namespace Logic
{
    public class RepairService
    {
        private readonly ServiceAccess _serviceaccess;
        private readonly RepairServiceDTO _repairService;

        public RepairService(ServiceAccess serviceaccess)
        {
            _serviceaccess = serviceaccess;
            _repairService = GetService();
            
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
    }
}

