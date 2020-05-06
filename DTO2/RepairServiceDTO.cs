using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RepairServiceDTO
    {
        public int MaxBigServicePerDay { get; set; }
        public int MaxSmallServicePerDay { get; set; }
        public List<TrackDTO> AllocatedTracks { get; set; }
        public List<TramDTO> AssignedTrams { get; set; }
    }
}
