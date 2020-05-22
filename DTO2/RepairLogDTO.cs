using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RepairLogDTO
    {
        public int Id { get; set; }
        public RepairServiceDTO RepairService { get; set; }
        public TramDTO Tram { get; set; }
        public UserDTO User { get; set; }
        public DateTime RepairDate { get; set; }
        public ServiceType ServiceType { get; set; }
        public bool Occured { get; set; }
        public string RepairMessage { get; set; }
        public bool WaitingList { get; set; }

        public RepairLogDTO(int id, RepairServiceDTO repairService, TramDTO tram, UserDTO user, DateTime repairDate, ServiceType serviceType, bool occured, string repairMessage, bool waitingList)
        {
            Id = id;
            RepairService = repairService;
            Tram = tram;
            User = user;
            RepairDate = repairDate;
            ServiceType = serviceType;
            Occured = occured;
            RepairMessage = repairMessage;
            WaitingList = waitingList;
        }
        public RepairLogDTO(RepairServiceDTO repairService, TramDTO tram, ServiceType serviceType,bool occured, bool waitingList)
        {
            RepairService = repairService;
            Tram = tram;
            ServiceType = serviceType;
            WaitingList = waitingList;
            Occured = occured;
        }
        public RepairLogDTO(RepairServiceDTO repairService, TramDTO tram, ServiceType serviceType, bool occured, bool waitingList, string repairMessage)
        {
            RepairService = repairService;
            Tram = tram;
            ServiceType = serviceType;
            WaitingList = waitingList;
            Occured = occured;
            RepairMessage = repairMessage;
        }
    }
}
