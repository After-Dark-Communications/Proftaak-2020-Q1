using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ServiceViewModel
    {
        public TramViewModel Tram { get; set; }
        public UserViewModel User { get; set; }
        public DateTime RepairDate { get; set; }
        public ServiceTypeEnum ServiceType { get; set; }
        public bool Occured { get; set; }
        public string RepairMessage { get; set; }
        public List<ServiceViewModel> AllServices { get; set; }

        public enum ServiceTypeEnum
        {
            Kleine_Beurt,
            Grote_Beurt,
            Nood_Beurt,
            Kleine_Reparatie,
            Grote_Eeparatie,
            Nood_Reparatie
        }
    }
}
