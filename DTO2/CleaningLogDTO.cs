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
        public DateTime CleaningDate { get; set; }
        public ServiceType CleaningType { get; set; }
        public bool Occured { get; set; }

        public CleaningLogDTO()
        {
            
        }

        public CleaningLogDTO(int id, CleaningServiceDTO cleanService, TramDTO tram, UserDTO user, DateTime cleaningDate, ServiceType cleaningType, bool occured)
        {
            Id = id;
            CleaningService = cleanService;
            Tram = tram;
            User = user;
            CleaningDate = cleaningDate;
            CleaningType = cleaningType;
            Occured = occured;
        }
        public CleaningLogDTO(CleaningServiceDTO cleanService, TramDTO tram, UserDTO user, DateTime cleaningDate, ServiceType cleaningType, bool occured)
        {
            CleaningService = cleanService;
            Tram = tram;
            User = user;
            CleaningDate = cleaningDate;
            CleaningType = cleaningType;
            Occured = occured;
        }

        public CleaningLogDTO(CleaningServiceDTO cleaningService, TramDTO tram, ServiceType cleaningType, bool occured)
        {
            CleaningService = cleaningService;
            Tram = tram;
            CleaningType = cleaningType;
            Occured = occured;
        }

        public CleaningLogDTO(CleaningServiceDTO cleanService, TramDTO tram, ServiceType cleaningType)
        {
            CleaningService = cleanService;
            Tram = tram;
            CleaningType = cleaningType;
        }

        public CleaningLogDTO(CleaningServiceDTO cleaning, TramDTO tram, DateTime cleaningDate, ServiceType serviceType, bool occured)
        {
            CleaningService = cleaning;
            Tram = tram;
            CleaningDate = cleaningDate;
            CleaningType = serviceType;
            Occured = occured;
        }
    }
}
