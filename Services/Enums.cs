using System;

namespace Services
{
    public enum TramStatus
    {
        Defect,
        Cleaning,
        Shift,
        Depot
    }
    public enum TramType
    {
        Combino,
        DoubleCombino,
        ElevenG,
        TwelveG,
        TrainingTram
    }
    public enum SectorType
    {
        Normal,
        UnPreferred,
        Blocked
    }
}
