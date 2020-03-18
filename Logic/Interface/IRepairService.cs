using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interface
{
    interface IRepairService : IService
    {
        void RepairTram(Tram tram);
    }
}
