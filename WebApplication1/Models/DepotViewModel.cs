using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DepotViewModel
    {
        public ICollection<UserViewModel> Users { get; set; }
        private ICollection<TrackViewModel> DepotTracks { get; set; }
    }
}
