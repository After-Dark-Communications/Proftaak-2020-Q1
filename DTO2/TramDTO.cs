using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class TramDTO
    {
        public int Id { get; set; }
        public TramType Type { get; set; }
        public List<StatusDTO> Status { get; set; }
        public string TramNumber { get; set; }
        private List<TrackDTO> PreferredTracksList { get; set; }
        public DateTime CleaningDateSmallService { get; set; }
        public DateTime CleaningDateBigService { get; set; }
        public DateTime RepairDateSmallService { get; set; }
        public DateTime RepairDateBigService { get; set; }
        public TramDTO()
        {
            Status = new List<StatusDTO>();
        }
    }
}
