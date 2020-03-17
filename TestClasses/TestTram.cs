using System;
using System.Collections.Generic;
using System.Text;

namespace TestClasses
{
    class TestTram
    {
        public string Number { get; private set; }
        public string Type { get; private set; } // gaan we hier nog objecten/ soorten voor maken
        public TestEnums.TramStatus Status { get; set; }

        public TestTram(string number, string type)
        {
            Number = number;
            Type = type;
            Status = TestEnums.TramStatus.Stored;
        }
    }
}
