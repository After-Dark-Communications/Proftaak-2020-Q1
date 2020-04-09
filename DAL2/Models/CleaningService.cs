using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class CleaningService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SmallCleansPerDay { get; set; }
        public int BigCleansPerDay { get; set; }
    }
}
