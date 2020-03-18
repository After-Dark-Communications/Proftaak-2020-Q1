using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class Service
    {
        public abstract int MaxSmallServicePerDay { get; set; }
        public abstract int MaxBigServicePerDay { get; set; }
        public void AddTram(Tram tram)
        {
            throw new NotImplementedException();
        }

        public bool BigService(Tram tram)
        {
            throw new NotImplementedException();
        }

        public void SmallService(Tram tram)
        {
            throw new NotImplementedException();
        }
    }
}
