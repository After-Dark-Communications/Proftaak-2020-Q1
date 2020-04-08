using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Sector
    {
        private ICollection<Tram> TramsOnSector;

        public void AddTram(Tram tram)
        {
            throw new NotImplementedException();
            //voeg tram toe aan database
            //voeg tram toe aan icollection
        }

        public bool CheckIfSectorIsEmpty()
        {
            throw new NotImplementedException();
        }

        public Tram GetTram()
        {
            throw new NotImplementedException();
        }

        //check if there is already a tram on this sector
    }
}
