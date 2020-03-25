using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SectorDTO
    {
        public int Id { get; set; }
        public TramDTO Tram { get; set; }
        public int TrackNumber { get; set; }
        public int SectorPosition { get; set; }
    }
}
