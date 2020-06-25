using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class CleaningLogDTO
    {
        public int Id { get; set; }
        public CleaningServiceDTO CleaningService { get; set; }
        public TramDTO Tram { get; set; }
        public UserDTO User { get; set; }
        public DateTime Date { get; set; }
        public ServiceType ServiceType { get; set; }
        public bool Occured { get; set; }

        public CleaningLogDTO()
        {
            
        }

        public CleaningLogDTO(int id, CleaningServiceDTO cleanService, TramDTO tram, UserDTO user, DateTime date, ServiceType serviceType, bool occured)
        {
            Id = id;
            CleaningService = cleanService;
            Tram = tram;
            User = user;
            Date = date;
            ServiceType = serviceType;
            Occured = occured;
        }
        public CleaningLogDTO(CleaningServiceDTO cleanService, TramDTO tram, UserDTO user, DateTime date, ServiceType serviceType, bool occured)
        {
            CleaningService = cleanService;
            Tram = tram;
            User = user;
            Date = date;
            ServiceType = serviceType;
            Occured = occured;
        }

        public CleaningLogDTO(CleaningServiceDTO cleaningService, TramDTO tram, ServiceType serviceType, bool occured)
        {
            CleaningService = cleaningService;
            Tram = tram;
            ServiceType = serviceType;
            Occured = occured;
        }

        public CleaningLogDTO(CleaningServiceDTO cleanService, TramDTO tram, ServiceType serviceType)
        {
            CleaningService = cleanService;
            Tram = tram;
            ServiceType = serviceType;
        }
    }
}
