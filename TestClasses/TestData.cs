using System;
using System.Collections.Generic;

namespace TestClasses
{
    public static class TestData
    {
        public static List<Track> Tracks = new List<Track>();
        public static readonly Tram tramA = new Tram("2201", "Combino");
        private static readonly string[] possibleTramNumbers = { "2001", "2002", "2003", "901", "902", "903", "2202", "2203", "817" };
        private static readonly string[] possibleTramTypes = { "Combino", "11G", "12G", "DubbleCombino", "Training" };
        private static Enums.TramStatus[] possibleStatusses = { Enums.TramStatus.InUse, Enums.TramStatus.NeedsCleaning, Enums.TramStatus.NeedsRepair, Enums.TramStatus.Stored };
        public static Tram GetRandomTram()
        {
            Random rnd = new Random();
            int rnd1 = rnd.Next(possibleTramNumbers.Length);
            int rnd2 = rnd.Next(possibleTramTypes.Length);
            int rnd3 = rnd.Next(possibleStatusses.Length);
            Tram returnTram = new Tram(possibleTramNumbers[rnd1], possibleTramTypes[rnd2]);
            returnTram.Status = possibleStatusses[rnd3];

            return returnTram;
        }

        public static void InitTestData() // Just a function to fill the Tracks with sectors. Alter when needed
        {
            Tracks.Add(new Track(4, 38));
            Tracks.Add(new Track(4, 37));
            Tracks.Add(new Track(4, 36));
            Tracks.Add(new Track(4, 35));
            Tracks.Add(new Track(4, 34));
            Tracks.Add(new Track(4, 32));
            Tracks.Add(new Track(3, 31));
            Tracks.Add(new Track(3, 30));

            Tracks.Add(new Track(7, 40));

            Tracks.Add(new Track(4, 41));
            Tracks.Add(new Track(4, 42));
            Tracks.Add(new Track(4, 43));
            Tracks.Add(new Track(4, 44));
            Tracks.Add(new Track(7, 45));

            Tracks.Add(new Track(5, 58));

            Tracks.Add(new Track(8, 57));
            Tracks.Add(new Track(8, 56));
            Tracks.Add(new Track(8, 55));
            Tracks.Add(new Track(7, 54));
            Tracks.Add(new Track(7, 53));
            Tracks.Add(new Track(7, 52));
            Tracks.Add(new Track(6, 51));
            Tracks.Add(new Track(5, 64));
            Tracks.Add(new Track(4, 63));
            Tracks.Add(new Track(3, 62));
            Tracks.Add(new Track(3, 61));

            Tracks.Add(new Track(5, 73));
            Tracks.Add(new Track(5, 74));
            Tracks.Add(new Track(4, 75));
            Tracks.Add(new Track(5, 76));
            Tracks.Add(new Track(5, 77));

            Tracks.Add(new Track(1, 12));
            Tracks.Add(new Track(1, 13));
            Tracks.Add(new Track(1, 14));
            Tracks.Add(new Track(1, 15));
            Tracks.Add(new Track(1, 16));
            Tracks.Add(new Track(1, 17));
            Tracks.Add(new Track(1, 18));
            Tracks.Add(new Track(1, 19));
            Tracks.Add(new Track(1, 20));
            Tracks.Add(new Track(1, 21));

            TestData.Tracks[0].AssignTramToSector(TestData.tramA);
        }
    }
}
