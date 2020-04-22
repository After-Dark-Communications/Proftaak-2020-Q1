using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using DAL;

namespace WebApplication1.Models
{
    public class TramViewModel
    {
        public int Id { get; set; }
        public string TramNumber { get; set; }
        public TramType Type { get; set; }
        public DateTime CleaningDateSmallService { get; set; }
        public DateTime CleaningDateBigService { get; set; }
        public DateTime RepairDateSmallService { get; set; }
        public DateTime RepairDateBigService { get; set; }
        public List<StatusViewModel> Status { get; set; }
        private List<TrackViewModel> PreferredTracksList { get; set; }
    }
}
