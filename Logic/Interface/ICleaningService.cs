using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interface
{
    interface ICleaningService : IService
    {
        void CleanTram(Tram tram);
    }
}
