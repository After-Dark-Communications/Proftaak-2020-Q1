﻿using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;
using System.Linq;
using Services;

namespace Logic
{
    public class Depot
    {
        Track _tracklogic;
        Tram _tramlogic;
        Sector _sectorLogic;
        RepairService _repairServicelogic;
        
        private readonly IDepotAccess _depotaccess;

        public Depot(Track tracklogic, Tram tramlogic,Sector sectorLogic,RepairService repairServiceLogic, IDepotAccess depotAccess)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
            this._depotaccess = depotAccess;
            this._sectorLogic = sectorLogic;
            this._repairServicelogic = repairServiceLogic;
        }

        public void ReceiveTram(string tramNumber, bool repairStatus, bool cleanStatus, string? repairMessage , DepotDTO depot) 
        {
            if (!IsTramAllreadyInDepot(tramNumber, depot, _sectorLogic, _tramlogic))
            {
               
               TramDTO tram = _tramlogic.GetTram(tramNumber);

                if (tram.DepotId == 1)
                {
                    changeTramStatus(tram, repairStatus, cleanStatus, _tramlogic);
                    if (repairMessage != null)
                    {
                        _repairServicelogic.CreateRepairLogDefect(tram, repairMessage);
                    }
                    else
                    {
                        _repairServicelogic.DetermineRepairType(tram);
                    }
                    AllocationManager.AllocateTramToTrack(tram, depot.DepotTracks, _tracklogic, _tramlogic, _repairServicelogic);
                }
                else
                {
                    //return the tram
                }
            }
            else
            {
                //return the tram
            }
        }



        public bool IsTramAllreadyInDepot(string tramNumber, DepotDTO depot, Sector _sectorLogic, Tram _tramLogic)
        {
            if (_tramlogic.IsTramAllreadyInDepot(tramNumber))
            {
                return true;
            }
            return false;
        }

        public int AmountOfRLTramsInDepot(DepotDTO depot, List<TramDTO> trams)
        {
            if (trams.Any(t => t.DepotId == 2 && IsTramAllreadyInDepot(t.TramNumber, depot, _sectorLogic, _tramlogic)))
            {
                IEnumerable<TramDTO> RLTrams = trams.Where(t => t.DepotId == 2 && IsTramAllreadyInDepot(t.TramNumber, depot, _sectorLogic, _tramlogic));
                return RLTrams.Count();
            }
            return 0;
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
        public DepotDTO Read(int key)
        {
           return _depotaccess.Read(key);
        }


    }
}
