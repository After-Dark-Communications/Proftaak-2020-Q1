using System;
using System.Collections.Generic;
using System.Reflection;

namespace DTO
{
    public class Tram
    {
        public string SerialNumber;

        public ICollection<Service> Status;
    }
}
