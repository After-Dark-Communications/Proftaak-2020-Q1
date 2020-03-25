using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Tram
    {
        [Key]
        public int Id { get; set; }
        public TramType Type { get; set; }
        public TramStatus Status { get; set; }
    }

    public enum TramType
    {
        Combino,
        DoubleCombino,
        G11,
        G12,
        TrainingTram,
    }

    public enum TramStatus
    {
        Defect,
        Cleaning,
        Shift,
        Depot,
    }
}
