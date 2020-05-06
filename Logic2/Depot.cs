using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;
using Services;

namespace Logic
{
    public class Depot
    {
        Track _tracklogic;
        Tram _tramlogic;

        private readonly IDepotAccess _depotaccess;

        public Depot(Track tracklogic, Tram tramlogic, IDepotAccess depotAccess)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
            this._depotaccess = depotAccess;
        }

        public void ReceiveTram(string tramNumber, bool repairstatus, bool cleanstatus, string statusDescription, DepotDTO depot) 
        {
            if (CheckIfTramIsAllowed(tramNumber, _tramlogic))
            {
                TramDTO tram = _tramlogic.GetTram(tramNumber);
                ChangeTramStatus(tram, repairstatus, cleanstatus, _tramlogic, statusDescription);
                //AllocationManager.AllocateTramToService(tram, _repairServiceLogic, _cleaningServiceLogic);
                AllocationManager.AllocateToRandomTrack(tram, depot.DepotTracks, _tracklogic);
            }
            else
            {
                //not needed yet
            }
        }

        private bool CheckIfTramIsAllowed(string tramNumber, Tram _tramlogic)
        {
            return _tramlogic.CheckIfTramExists(tramNumber);
        }

        private void ChangeTramStatus(TramDTO tram, bool repairstatus, bool cleanstatus, Tram _tramlogic, string statusDescription)
        {

            StatusDTO statusInDepot = new StatusDTO();
            statusInDepot.Status = TramStatus.Depot;
            _tramlogic.AddStatus(statusInDepot, tram);

            if (repairstatus)
            {
                StatusDTO status = new StatusDTO();
                status.Status = TramStatus.Defect;
                status.Description = statusDescription;
                _tramlogic.AddStatus(status, tram);
            }
            if (cleanstatus)
            {

                StatusDTO status = new StatusDTO();
                status.Status = TramStatus.Cleaning;
                _tramlogic.AddStatus(status, tram);
            }
        }
        public void Update(DepotDTO depot)
        {
            _depotaccess.Update(depot);
        }
        public DepotDTO Read(int key)
        {
           return _depotaccess.Read(key);
        }
    }
}
