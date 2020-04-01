using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DTO;
using Services;

namespace Logic
{
    public class Tram {

        private readonly ITramAccess _tramAccess;
        public string SerialNumber { get; set; }
        //public TramType Type { get; set; }
        public ICollection<TramStatus> Status { get; private set; }
        private ICollection<Track> PreferredTracksList { get; set; }

        public Tram(ITramAccess tramAccess)
        {
            Status = new List<TramStatus>();
            _tramAccess = tramAccess;
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
        public TramDTO GetTram(int key)
        {
            return _tramAccess.Read(key);
        }
    }
}
