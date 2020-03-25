using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Track
    {
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public TramType TramType { get; set; }
        public ICollection<Sector> Sectors { get; set; }
    }
}
