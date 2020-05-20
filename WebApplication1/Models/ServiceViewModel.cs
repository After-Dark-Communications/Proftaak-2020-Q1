using System;

namespace WebApplication1.Models
{
    public class ServiceViewModel
    {
        public string TramNumber { get; set; }
        public string TackNumber { get; set; }
        public int CleaningType { get; set; }
        public string CleaningStatus { get; set; }
        public string RepairType { get; set; }
        public string RepairStatus { get; set; }

    }
}
