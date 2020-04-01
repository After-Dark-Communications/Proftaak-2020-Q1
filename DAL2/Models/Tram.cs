using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Services;

namespace DAL.Models
{
    public class Tram
    {
        [Key]
        public int Id { get; set; }
        public TramType Type { get; set; }
        public TramStatus Status { get; set; }
        public int TramNumber { get; set; }
    }

}
