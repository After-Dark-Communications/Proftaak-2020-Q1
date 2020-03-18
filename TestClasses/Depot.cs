using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    public class Depot
    {
        public List<Track> Tracks = new List<Track>();

        public Depot()
        {
            InitTestData();
        }

        private void InitTestData() // Just a function to fill the Tracks with sectors. Alter when needed
        {
            int numberOfTracks = 4; //want more tracks? Alter this number
            int[] numerOfSectorsPerTrack = { 4, 5, 6, 7 }; //change sectors per track
            int trackNumber = 38;
            for (int i = 0; i < numberOfTracks; i++)
            {
                Tracks.Add(new Track(numerOfSectorsPerTrack[i], trackNumber));
                trackNumber--;
            }

            Tracks[0].AssignTramToSector(TestData.tramA);
        }


    }
}
