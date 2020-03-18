using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    public class Track
    {
        public int TrackNumber { get; private set; }
        public Sector[] Sectors { get; private set; } //for now returns array 

        public Track(int numberOfSectors, int trackNumber)
        {
            Sectors = new Sector[numberOfSectors];
            for (int i = 0; i < numberOfSectors; i++)
            {
                Sectors[i] = new Sector();
            }

            TrackNumber = trackNumber;
        }


        public Tram GetTram(string tramNumber)
        {
            foreach (Sector sector in Sectors)
            {
                Tram tram = sector.GetTram(tramNumber);
                if (tram != null)
                {
                    return tram;
                }
            }

            return null;
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
