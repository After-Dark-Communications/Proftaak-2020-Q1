using DAL.Concrete;
using DTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;


namespace Logic
{
    public class RepairService
    {
        private readonly ServiceAccess _serviceaccess;

        public RepairService(ServiceAccess serviceaccess)
        {
            _serviceaccess = serviceaccess;
        }
        public void RepairTram(TramDTO tram)
        {
            if(!CanRepairTram(GetService()))
            {
                DateTime RepairDate = DateTime.Now;
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
            return _serviceaccess.Read();
        }
    }
}

