using System;
using System.Collections.Generic;
using Logic.Models;

namespace Logic
{
    public class Tram {

        public string SerialNumber { get; set; }
        public TramType Type { get; set; }
        public ICollection<TramStatus> Status { get; private set; }
        private ICollection<Track> PreferredTracksList { get; set; }

        public Tram()
        {
            Status = new List<TramStatus>();
        }

        public void AddStatus(TramStatus newStatus)
        {
            if (!Status.Contains(newStatus))
            {
                Status.Add(newStatus);
            }
        }

        public void DeleteStatus(TramStatus stat)
        {
            Status.Remove(stat);
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
