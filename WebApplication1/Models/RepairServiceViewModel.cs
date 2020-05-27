using Services;
using System;

namespace WebApplication1.Models
{
    public class RepairServiceViewModel
    {
        public string TramNumber { get; set; }
        public int TrackNumber { get; set; }
        public int SectorNumber { get; set; }
        public ServiceType RepairType { get; set; }
        public bool Occured { get; set; }
        public string RepairMessage { get; set; }
        public UserViewModel User { get; set; }
        public DateTime RepairDate { get; set; }
    }
}
