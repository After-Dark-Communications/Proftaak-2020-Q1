using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class TrackDTO
    {
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public TramType TramType { get; set; }
        public int PreferredTrackLine { get; set; }
        public ICollection<SectorDTO> Sectors { get; set; }

        public TrackDTO()
        {
            Sectors = new List<SectorDTO>();
        }
    }
}
