using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Sector
    {
        public int Id { get; set; }
        public Tram Tram { get; set; }
        public int TrackNumber { get; set; }
        public int SectorPosition { get; set; }
    }
}
