using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    public class Tram
    {
        public string Number { get; private set; }
        public string Type { get; private set; } // gaan we hier nog objecten/ soorten voor maken
        public Enums.TramStatus Status { get; set; }

        public Tram(string number, string type)
        {
            Number = number;
            Type = type;
            Status = Enums.TramStatus.Stored;
        }

    }
}
