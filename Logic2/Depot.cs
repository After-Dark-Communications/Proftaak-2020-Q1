using System;
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

        public void ReceiveTram(string tramNumber, bool repairstatus, bool cleanstatus, string statusDescription, DepotDTO depot) 
        {
            if (!IsTramAllreadyInDepot(tramNumber, depot, _sectorLogic, _tramlogic))
            {
               
               TramDTO tram = _tramlogic.GetTram(tramNumber);
               changeTramStatus(tram, repairstatus, cleanstatus, _tramlogic, statusDescription);
                if (tram.DepotId == 1)
                {
                    AllocationManager.AllocateTramToTrack(tram, depot.DepotTracks, _tracklogic, _tramlogic, _repairServicelogic);
                }
                else if (AmountOfRLTramsInDepot(depot, depot.TramsInDepot) < 3)
                {
                        AllocationManager.AllocateTramToTrack(tram, depot.DepotTracks, _tracklogic, _tramlogic, _repairServicelogic);
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
            IEnumerable<TramDTO> RLTrams = trams.Where(t => t.DepotId == 2 && IsTramAllreadyInDepot(t.TramNumber, depot, _sectorLogic, _tramlogic));
            return RLTrams.Count();
        }

        private void changeTramStatus(TramDTO tram, bool repairstatus, bool cleanstatus, Tram _tramlogic, string statusDescription)
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
