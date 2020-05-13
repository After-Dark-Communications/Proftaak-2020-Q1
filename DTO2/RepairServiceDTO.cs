using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RepairServiceDTO
    {
        public int SmallRepairsPerDay { get; set; }
        public int BigRepairsPerDay { get; set; }
        public string Location { get; set; }

        public RepairServiceDTO(int smallRepairsPerDay, int bigRepairsPerDay, string location)
        {
            SmallRepairsPerDay = smallRepairsPerDay;
            BigRepairsPerDay = bigRepairsPerDay;
            Location = location;
        }
    }
}
