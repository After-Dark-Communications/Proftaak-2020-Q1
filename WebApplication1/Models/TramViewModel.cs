using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TramViewModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }

        public TramType Type { get; set; }
        public TramStatus Status { get; set; }
    }
    public enum TramStatus
    {
        Defect,
        Cleaning,
        Shift,
        Depot,
    }
    public enum TramType
    {
        Combino,
        DoubleCombino,
        ElevenG,
        TwelveG,
        TrainingTram,
    }
}
