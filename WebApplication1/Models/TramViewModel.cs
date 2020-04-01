using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;

namespace WebApplication1.Models
{
    public class TramViewModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }

        public TramType Type { get; set; }
        public TramStatus Status { get; set; }
    }
}
