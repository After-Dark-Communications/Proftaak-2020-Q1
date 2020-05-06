using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;

namespace WebApplication1.Models
{
    public class SectorViewModel
    {
        public int Id { get; set; }
        public TramViewModel Tram { get; set; }
        public int TrackNumber { get; set; }
        public int SectorPosition { get; set; }
        public SectorType SectorType { get; set; }
    }
}
