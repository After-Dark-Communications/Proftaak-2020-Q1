using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DepotViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public List<TrackViewModel> DepotTracks { get; set; }
    }
}
