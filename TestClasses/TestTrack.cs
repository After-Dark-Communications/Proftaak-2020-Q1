using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    public class TestTrack
    {
        
        public TestSector[] Sectors { get; private set; } //for now returns array 

        public TestTrack(int numberOfSectors)
        {
            Sectors = new TestSector[numberOfSectors];
            for (int i = 0; i < numberOfSectors; i++)
            {
                Sectors[i] = new TestSector();
            }
        }

        public bool AssignTramToSector(TestTram tram)
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
