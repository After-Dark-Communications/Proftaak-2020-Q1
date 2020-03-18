using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Logic;

namespace DAL.Models
{
    public class TramDal
    {
        [Key]
        public int Id;
        public TramType TramType { get; set; }
        public TramStatus TramStatus { get; set; }
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
