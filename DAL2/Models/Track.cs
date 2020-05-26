using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DAL.Models
{
    public class Track
    {
        public int Id { get; set; }
        public TramType PreferredTramType { get; set; }
        public TrackType TrackType { get; set; }
        public int TrackNumber { get; set; }
        public int RepairServiceId { get; set; } //Foreign Keys
        public int PreferredLine { get; set; }
        public ICollection<Sector> Sectors { get; set; }
    }
}
