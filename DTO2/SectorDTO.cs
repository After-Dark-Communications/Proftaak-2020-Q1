using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class SectorDTO
    {
        public int Id { get; set; }
        public TramDTO Tram { get; set; }
        public int TrackNumber { get; set; }
        public int SectorPosition { get; set; }
        public SectorType SectorType { get; set; }
        public SectorDTO()
        {

        }
        public SectorDTO(int trackNumber,int sectorPosition)
        {
            TrackNumber = trackNumber;
            SectorPosition = sectorPosition; 
        }
    }
}
