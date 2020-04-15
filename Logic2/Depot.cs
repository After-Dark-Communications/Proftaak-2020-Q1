using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using Services;

namespace Logic
{
    public class Depot
    {
        public ICollection<UserDTO> Users { get; set; }
        private ICollection<TrackDTO> DepotTracks { get; set; }
        Track _tracklogic;
        Tram _tramlogic;
        RepairService _repairServiceLogic;
        CleaningService _cleaningServiceLogic;

        public Depot(Track tracklogic, Tram tramlogic, RepairService repairServiceLogic, CleaningService _cleaningServiceLogic)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
            this._repairServiceLogic = repairServiceLogic;
            this._cleaningServiceLogic = _cleaningServiceLogic;
        }

        public void ReceiveTram(int tramNumber, bool repairstatus, bool cleanstatus) 
        {
            if (CheckIfTramIsAllowed(tramNumber, _tramlogic))
            {
                TramDTO tram = _tramlogic.GetTram(tramNumber);
                changeTramStatus(tram, repairstatus, cleanstatus, _tramlogic);
                AllocationManager.AllocateTramToService(tram, _repairServiceLogic, _cleaningServiceLogic);
                AllocationManager.AllocateTramToTrack(tram, DepotTracks, _tracklogic, _repairServiceLogic);
            }
        }

        private bool CheckIfTramIsAllowed(int tramNumber, Tram _tramlogic)
        {
            _tramlogic.CheckIfTramExists(tramNumber);
            throw new NotImplementedException();
        }

        private void changeTramStatus(TramDTO tram, bool repairstatus, bool cleanstatus, Tram _tramlogic)
        {
            //dit moet hoogstwss anders
            StatusDTO statusDepot = new StatusDTO();
            statusDepot.Status = TramStatus.Depot;
            _tramlogic.AddStatus(statusDepot, tram);

            if (repairstatus)
            {
                //dit moet hoogstwss anders
                StatusDTO status = new StatusDTO();
                status.Status = TramStatus.Defect;
                _tramlogic.AddStatus(status, tram);
            }
            if (cleanstatus)
            {
                //dit moet hoogstwss anders
                StatusDTO status = new StatusDTO();
                status.Status = TramStatus.Cleaning;
                _tramlogic.AddStatus(status, tram);
            }
        }

    }
}
