using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class TramDTO
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public TramType Type { get; set; }
        public ICollection<StatusDTO> Status { get; set; }
        private ICollection<TrackDTO> PreferredTracksList { get; set; }
    }
}
