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
        Sector _sectorLogic;
        
        private readonly IDepotAccess _depotaccess;

        public Depot(Track tracklogic, Tram tramlogic,Sector sectorLogic, IDepotAccess depotAccess)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
            this._depotaccess = depotAccess;
            this._sectorLogic = sectorLogic;
        }

        public void ReceiveTram(string tramNumber, bool repairstatus, bool cleanstatus, string statusDescription, DepotDTO depot) 
        {
            if (!IsTramAllreadyInDepot(tramNumber, depot, _sectorLogic, _tramlogic))
            {
                //RL of RH?

               // TramDTO tram = _tramlogic.GetTram(tramNumber);
               // changeTramStatus(tram, repairstatus, cleanstatus, _tramlogic, statusDescription);
                //AllocationManager.AllocateTramToService(tram, _repairServiceLogic, _cleaningServiceLogic);
               
            }
            else
            {
                //return the tram
            }
        }

        public bool IsTramAllreadyInDepot(string tramNumber, DepotDTO depot, Sector _sectorLogic, Tram _tramLogic)
        {
           //foreach (TrackDTO track in depot.DepotTracks)
           // {
           //     foreach (SectorDTO sector in track.Sectors)
           //     {
           //         if (_sectorLogic.CheckIfSectorIsEmpty(sector))
           //         {
           //             if (_sectorLogic.GetTram(sector) == _tramlogic.GetTram(tramNumber))
           //             {
           //                 return true;
           //             }
           //         }
           //     }
           // }

            if (_tramlogic.IsTramAllreadyInDepot(tramNumber))
            {
                return true;
            }
            return false;
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
