using System;
using System.Collections.Generic;

namespace Logic
{
    public class Tram {

        public string SerialNumber { get; set; }
        public ICollection<TramStatus> Status { get; set; }
        private ICollection<Track> PreferredTracksList { get; set; }

        public void ChangeStatus()
        {
            throw new NotImplementedException();
        }

        public void GetType()
        {
            throw new NotImplementedException();
        }

        public void GetServiceHistory()
        {
            throw new NotImplementedException();
        }
    }
}
