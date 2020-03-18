using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class CleaningService : Service
    {
        public override int MaxBigServicePerDay { get; set; }
        public override int MaxSmallServicePerDay { get; set; }

        public void CleanTram(Tram tram)
        {
            throw new NotImplementedException();
        }
    }
}
