using System;
using System.Collections.Generic;

namespace TestClasses
{
    public static class TestData
    {
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
    }
}
