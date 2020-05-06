using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;


namespace Logic
{
    public class CleaningService
    {
        private readonly ServiceAccess _serviceaccess;
        private readonly CleaningServiceDTO _repairService;

        public CleaningService(ServiceAccess serviceaccess)
        {
            _serviceaccess = serviceaccess;
            _repairService = GetService();

        }
        public void SetSmallCleanTram(TramDTO tram)
        {
            if (CanCleanTram(_repairService))
            {
                DateTime RepairDate = DateTime.Now;
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Cleaning);
                tram.RepairDateSmallService = RepairDate;
                _repairService.MaxSmallServicePerDay--;
            }
        }
        public void SetLargeRepairTram(TramDTO tram)
        {
            if (CanCleanTram(_repairService))
            {
                DateTime RepairDate = DateTime.Now;
                tram.Status.RemoveAll(repair => repair.Status == Services.TramStatus.Cleaning);
                tram.RepairDateBigService = RepairDate;
                _repairService.MaxBigServicePerDay--;
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
    }
}
