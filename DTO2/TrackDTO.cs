using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class TrackDTO
    {
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public TramType TramType { get; set; }
        public ICollection<SectorDTO> Sectors { get; set; }
    }
}
