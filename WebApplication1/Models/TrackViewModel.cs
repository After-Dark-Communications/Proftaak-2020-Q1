using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TrackViewModel
    {
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public TramType TramType { get; set; }
        public ICollection<SectorViewModel> Sectors { get; set; }
    }
}
