using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CleaningServiceViewModel
    {
        public string TramNumber { get; set; }
        public int TrackNumber { get; set; }
        public string SectorNumber { get; set; }
        public ServiceType CleaningType { get; set; }
        public bool Occured { get; set; }
        public UserViewModel User { get; set; }
        public DateTime CleaningDate { get; set; }
        public IEnumerable<UserViewModel> SchoonMakers { get; set; }
    }
}
