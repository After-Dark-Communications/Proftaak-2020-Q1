using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class TrackDTO
    {
        public int Id { get; set; }
        public string TrackNumber { get; set; }
        public TramType ? TramType { get; set; }
        public int PreferredTrackLine { get; set; }
        public List<SectorDTO> Sectors { get; set; }
        public TrackType Type { get; set; }

        public TrackDTO()
        {
            Sectors = new List<SectorDTO>();
        }
    }
}
