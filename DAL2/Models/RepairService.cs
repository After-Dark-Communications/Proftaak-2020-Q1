using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class RepairService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SmallRepairsPerDay { get; set; }
        public int BigRepairsPerDay { get; set; }
    }
}
