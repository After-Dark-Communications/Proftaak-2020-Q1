using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class TramDTO
    {
        public int Id { get; set; }
        public TramType Type { get; set; }
        public List<StatusDTO> Status { get; set; }
        public string TramNumber { get; set; }
        public int DepotId { get; set; }
        public int Line { get; set; }
        private List<TrackDTO> PreferredTracksList { get; set; }
        public DateTime CleaningDateSmallService { get; set; }
        public DateTime CleaningDateBigService { get; set; }
        public DateTime RepairDateSmallService { get; set; }
        public DateTime RepairDateBigService { get; set; }
        public TramDTO()
        {
            Status = new List<StatusDTO>();
        }

        public TramDTO(TramType type, List<StatusDTO> status, string tramNumber, List<TrackDTO> preferredTracksList, DateTime cleaningDateSmallService, DateTime cleaningDateBigService, DateTime repairDateSmallService, DateTime repairDateBigService)
        {
            Type = type;
            Status = status;
            TramNumber = tramNumber;
            PreferredTracksList = preferredTracksList;
            CleaningDateSmallService = cleaningDateSmallService;
            CleaningDateBigService = cleaningDateBigService;
            RepairDateSmallService = repairDateSmallService;
            RepairDateBigService = repairDateBigService;
        }

        public TramDTO(string tramNumber)
        {
            TramNumber = tramNumber;
        }
    }
}
