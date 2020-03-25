using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DepotDTO
    {
        public ICollection<UserDTO> Users { get; set; }
        private ICollection<TrackDTO> DepotTracks { get; set; }
    }
}
