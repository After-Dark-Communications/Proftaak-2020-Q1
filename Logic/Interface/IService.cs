using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interface
{
    interface IService
    {
        void AddTram(Tram tram);

        void SmallService(Tram tram);

        bool BigService(Tram tram);
    }
}
