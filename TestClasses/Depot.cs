using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    public class Depot
    {
        public List<Track> Tracks { get { return TestData.Tracks; } }
        public Depot()
        {
            TestData.InitTestData();
        }

        public bool PlaceTram(Tram newTram)
        {
            foreach (Track track in TestData.Tracks)
            {
                if (track.AssignTramToSector(newTram))
                {
                    return true;
                }
            }
            return false;
        }

        public bool PlaceTramRandomSector(Tram newTram)
        {
            Random rnd = new Random();
            int rndTrack = rnd.Next(TestData.Tracks.Count);
            if(TestData.Tracks[rndTrack].AssignTramToRandomSector(newTram))
            {
                return true;
            }
            
            return false;
        }

        public void MoveTramToOtherSector(Sector oldSector, Sector newSector)
        {
            if(oldSector.Trams[0] != null)
            {
                if (newSector.TryAddTram(oldSector.Trams[0]))
                {
                    oldSector.Trams.Clear();
                }
            }
            
        }

        public Tram GetTram(string tramNumber)
        {
            foreach (Track track in TestData.Tracks)
            {
                Tram tram = track.GetTram(tramNumber);
                if (tram != null)
                {
                    return tram;
                }
            }

            return null;
        }

        public Tram GetTram(int modelNumber)
        {
            return GetTram(modelNumber.ToString());
        }

        


    }
}
