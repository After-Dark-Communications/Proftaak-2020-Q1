using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IServiceAccess
    {
        public RepairServiceDTO ReadRepair();
        //public CleaningServiceDTO ReadCleaning();

        public void UpdateCleaning();
        public void UpdateRepair();
    }
}
