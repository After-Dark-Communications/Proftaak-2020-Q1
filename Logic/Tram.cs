using System;
using System.Collections.Generic;
using Logic.Models;

namespace Logic
{
    public class Tram {

        public string SerialNumber { get; set; }
        public TramType Type { get; set; }
        public ICollection<TramStatus> Status { get; set; }
        private ICollection<Track> PreferredTracksList { get; set; }

        public void AddStatus(TramStatus status)
        {
            //check if status is already in this tram
            //add status tram
            
            throw new NotImplementedException();
        }

        public void RemoveStatus(TramStatus status)
        {
            //check if status is in the tram
            //remove status
        }

        public void GetType()
        {
            //check the type of this tram
            throw new NotImplementedException();
        }

        public void GetServiceHistory()
        {
            //fetch service history from the database and return it?
            throw new NotImplementedException();
        }
    }
}
