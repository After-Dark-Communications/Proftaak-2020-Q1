using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CleaningLogDTO
    {
        public CleaningServiceDTO CleaningService { get; set; }
        public TramDTO Tram { get; set; }
        public UserDTO User { get; set; }
        public DateTime RepairDate { get; set; }
        public int ServiceType { get; set; }// todo enum
        public bool Occured { get; set; }
        public string RepairMessage { get; set; }

        public CleaningLogDTO(CleaningServiceDTO cleanService, TramDTO tram, UserDTO user, DateTime repairDate, int serviceType, bool occured, string repairMessage)
        {
            CleaningService = cleanService;
            Tram = tram;
            User = user;
            RepairDate = repairDate;
            ServiceType = serviceType;
            Occured = occured;
            RepairMessage = repairMessage;
        }
    }
}
