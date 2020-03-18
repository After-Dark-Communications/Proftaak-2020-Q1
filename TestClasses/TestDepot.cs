using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    class TestDepot
    {
        public List<TestTrack> Tracks
        {
            get { return TestData.Tracks; }
        }

        public TestDepot()
        {
            InitTestData();
        }

        public void InitTestData() // Just a function to fill the Tracks with sectors. Alter when needed
        {
            int numberOfTracks = 4; //want more tracks? Alter this number
            int[] numerOfSectorsPerTrack = { 4, 5, 6, 7 }; //change sectors per track

            for (int i = 0; i < numberOfTracks; i++)
            {
                TestData.Tracks.Add(new TestTrack(numerOfSectorsPerTrack[i]));
            }
        }

        public void ReceiveAndPlaceTram(TestTram tram)
        {

        }



    }
}
