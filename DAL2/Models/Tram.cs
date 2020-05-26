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
        public ICollection<Status> Status { get; set; }
        public string TramNumber { get; set; }
        public int DepotId { get; set; }
        public int Line { get; set; }
    }

}
