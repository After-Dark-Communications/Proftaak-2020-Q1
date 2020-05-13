using System;
using System.Collections.Generic;
using System.Text;
using DAL.Concrete;
using DTO;

namespace DAL.Interfaces
{
    public interface ICleaningServiceAccess : IGenAccess<CleaningServiceDTO>
    {
       public void StoreCleaningLog(CleaningLogDTO cleanLog);
        public void UpdateCleanLog(CleaningLogDTO cleanLog);

    }
}
