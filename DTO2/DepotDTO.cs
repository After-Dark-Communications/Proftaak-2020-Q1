using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DepotDTO
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public List<TrackDTO> DepotTracks { get; set; }
    }
}
