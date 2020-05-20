using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RepairServiceDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Location { get; set; }
        public bool Occured { get; set; }
        public DateTime RepairDate { get; set; }
        public int MaxBigServicePerDay { get; set; }
        public int MaxSmallServicePerDay { get; set; }
        public int ServiceType { get; set; }
        public List<TrackDTO> AllocatedTracks { get; set; }
        public List<TramDTO> AssignedTrams { get; set; }

        public RepairServiceDTO(string location)
        {
            Location = location;
        }

        public RepairServiceDTO(int smallRepairsPerDay, int bigRepairsPerDay, string location)
        {
            MaxBigServicePerDay = smallRepairsPerDay;
            MaxSmallServicePerDay = bigRepairsPerDay;
            Location = location;
        }
    }
}
