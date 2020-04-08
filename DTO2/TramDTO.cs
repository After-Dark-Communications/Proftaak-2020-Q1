using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class TramDTO
    {
        public int Id { get; set; }
        public TramType Type { get; set; }
        public ICollection<StatusDTO> Status { get; set; }
        public string TramNumber { get; set; }
        private ICollection<TrackDTO> PreferredTracksList { get; set; }
    }
}
