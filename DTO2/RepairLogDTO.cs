using Services;
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
        public ServiceType ServiceType { get; set; }
        public bool Occured { get; set; }
        public string RepairMessage { get; set; }

        public RepairLogDTO(RepairServiceDTO repairService, TramDTO tram, UserDTO user, DateTime repairDate, ServiceType serviceType, bool occured, string repairMessage)
        {
            RepairService = repairService;
            Tram = tram;
            User = user;
            RepairDate = repairDate;
            ServiceType = serviceType;
            Occured = occured;
            RepairMessage = repairMessage;
        }
        public RepairLogDTO(RepairServiceDTO repairService, TramDTO tram, ServiceType serviceType)
        {
            RepairService = repairService;
            Tram = tram;
            ServiceType = serviceType;
        }
    }
}
