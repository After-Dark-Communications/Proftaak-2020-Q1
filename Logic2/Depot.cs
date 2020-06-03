using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;
using System.Linq;
using Services;
using System.Diagnostics.CodeAnalysis;

namespace Logic
{
    public class Depot
    {
        Track _tracklogic;
        Tram _tramlogic;
        Sector _sectorLogic;
        RepairService _repairServicelogic;

        private readonly IDepotAccess _depotaccess;

        public Depot(Track tracklogic, Tram tramlogic, Sector sectorLogic, RepairService repairServiceLogic, IDepotAccess depotAccess)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
            this._depotaccess = depotAccess;
            this._sectorLogic = sectorLogic;
            this._repairServicelogic = repairServiceLogic;
        }

        public void ReceiveTram(string tramNumber, bool repairStatus, bool cleanStatus, string? repairMessage, DepotDTO depot)
        {
            if (!IsTramAllreadyInDepot(tramNumber, depot, _sectorLogic, _tramlogic))
            {

                if (IsTramFromRH(tramNumber))
                {
                    TramDTO tram = _tramlogic.GetTram(tramNumber);
                    changeTramStatus(tram, repairStatus, cleanStatus, _tramlogic);
                    if (repairStatus)
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

                    TramDTO tram = ReturnRLTram(tramNumber);
                    if (AmountOfTramsFromRL(depot) < 3 && repairStatus)
                    {
                        changeTramStatus(tram, repairStatus, cleanStatus, _tramlogic);
                        _repairServicelogic.CreateRepairLogDefect(tram, repairMessage);
                        AllocationManager.AllocateTramToTrack(tram, depot.DepotTracks, _tracklogic, _tramlogic, _repairServicelogic);
                    }
                    else
                    {
                        // return tram
                    }

                }
            }
            else
            {
                //return the tram
            }
        }

        public TramDTO ReturnRLTram(string tramNumber)
        {
            IEnumerable<TramDTO> allTrams = _tramlogic.GetAllTrams();
            if (allTrams.Where(t => t.TramNumber == tramNumber).Where(t => t.DepotId == 2).Any())
            {
                return allTrams.Where(t => t.TramNumber == tramNumber).Where(t => t.DepotId == 2).SingleOrDefault();
            }
            else
            {
                TramDTO tram = new TramDTO();
                tram.TramNumber = tramNumber;
                tram.DepotId = 2;
                _tramlogic.Create(tram);
                return tram;
              
            }
        }
        public int AmountOfTramsFromRL(DepotDTO depot)
        {
            IEnumerable<TramDTO> allTrams = _tramlogic.GetAllTrams();
            return allTrams.Where(t => t.DepotId == 2 && IsTramAllreadyInDepot(t.TramNumber, depot, _sectorLogic, _tramlogic)).Count();
        }
        public bool IsTramFromRH(string tramNumber)
        {
            IEnumerable<TramDTO> allTrams = _tramlogic.GetAllTrams();
            if (allTrams.Where(t => t.TramNumber == tramNumber).Where(t => t.DepotId == 1).Any())
            {
                return true;
            }
            return false;
        }

        public bool IsTramAllreadyInDepot(string tramNumber, DepotDTO depot, Sector _sectorLogic, Tram _tramLogic)
        {
            if (_tramlogic.IsTramAllreadyInDepot(tramNumber))
            {
                return true;
            }
            return false;
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

        public void DeleteStatus(TramDTO tram, TramStatus status)
        {

        }

        public void Update(DepotDTO depot)
        {
            _depotaccess.Update(depot);
        }
        public DepotDTO Read(int key)
        {
            return _depotaccess.Read(key);
        }
        public void TransferTram(string tramNumber, bool repairStatus, bool cleanStatus, string? repairMessage, DepotDTO depot)
        {
            TramDTO tram = _tramlogic.GetTram(tramNumber);
            _sectorLogic.RemoveTram(_sectorLogic.GetSector(_sectorLogic.GetSectorByTramNumber(tramNumber)));
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
    }
}
