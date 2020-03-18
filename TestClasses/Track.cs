using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    public class Track
    {
        
        public Sector[] Sectors { get; private set; } //for now returns array 

        public Track(int numberOfSectors)
        {
            Sectors = new Sector[numberOfSectors];
            for (int i = 0; i < numberOfSectors; i++)
            {
                Sectors[i] = new Sector();
            }
        }

        public bool AssignTramToSector(Tram tram)
        {
            for (int x = 0; x < Sectors.Length; x++)
            {
                if (Sectors[x].Trams.Count == 0)
                {
                    Sectors[x].AddTram(tram);
                    return true;
                }

            }

            return false;
        }
    }
}
