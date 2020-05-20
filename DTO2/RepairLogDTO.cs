using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RepairLogDTO
    {
        public RepairServiceDTO RepairService { get; set; }
        public TramDTO Tram { get; set; }
        public UserDTO User { get; set; }
        public DateTime RepairDate { get; set; }
        public int ServiceType { get; set; }// todo enum
        public bool Occured { get; set; }
        public string RepairMessage { get; set; }
        public int WaitingList { get; set; }

        public RepairLogDTO(RepairServiceDTO repairService, TramDTO tram, UserDTO user, DateTime repairDate, int serviceType, bool occured, string repairMessage, int waitingList)
        {
            RepairService = repairService;
            Tram = tram;
            User = user;
            RepairDate = repairDate;
            ServiceType = serviceType;
            Occured = occured;
            RepairMessage = repairMessage;
            WaitingList = waitingList;
        }
    }
}
