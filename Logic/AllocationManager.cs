using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class AllocationManager
    {
        public static void AllocateTramToService(Tram tram)
        {
            //if tram has a status, send to repair / cleaning service
            throw new NotImplementedException();
        }

        public static void AllocateTramToTrack(Tram tram)
        {
            //if status defect send to a repairservice track. check each time if it's possible
            //otherwise send to a normal track. check each time if it's possible
            throw new NotImplementedException();
        }
    }
}
