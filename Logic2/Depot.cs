﻿using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;
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
        private readonly IDepotAccess _depotaccess;

        public Depot(Track tracklogic, Tram tramlogic, RepairService repairServiceLogic, CleaningService _cleaningServiceLogic, IDepotAccess depotAccess)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
            this._repairServiceLogic = repairServiceLogic;
            this._cleaningServiceLogic = _cleaningServiceLogic;
            this._depotaccess = depotAccess;
        }

        public void ReceiveTram(string tramNumber, bool repairstatus, bool cleanstatus) 
        {
            if (CheckIfTramIsAllowed(tramNumber, _tramlogic))
            {
                TramDTO tram = _tramlogic.GetTram(tramNumber);
                changeTramStatus(tram, repairstatus, cleanstatus, _tramlogic);
                AllocationManager.AllocateTramToService(tram, _repairServiceLogic, _cleaningServiceLogic);
                AllocationManager.AllocateTramToTrack(tram, DepotTracks, _tracklogic, _repairServiceLogic);
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

        private void changeTramStatus(TramDTO tram, bool repairstatus, bool cleanstatus, Tram _tramlogic)
        {

            StatusDTO statusInDepot = new StatusDTO();
            statusInDepot.Status = TramStatus.Depot;
            _tramlogic.AddStatus(statusInDepot, tram);

            if (repairstatus)
            {

                StatusDTO status = new StatusDTO();
                status.Status = TramStatus.Defect;
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
    }
}
