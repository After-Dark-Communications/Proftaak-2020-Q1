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
        private readonly Track _tracklogic;
        private readonly Tram _tramlogic;
        private readonly Sector _sectorLogic;
        private readonly RepairService _repairServicelogic;
        private readonly CleaningService _cleaningServiceLogic;
        private readonly IDepotAccess _depotaccess;
        private readonly ITramAccess _tramAccess;

        public Depot(Track tracklogic, Tram tramlogic, Sector sectorLogic, RepairService repairServiceLogic, IDepotAccess depotAccess, CleaningService cleaningService)
        {
            this._tracklogic = tracklogic;
            this._tramlogic = tramlogic;
            this._depotaccess = depotAccess;
            this._sectorLogic = sectorLogic;
            this._repairServicelogic = repairServiceLogic;
            this._cleaningServiceLogic = cleaningService;
        }

        public void ReceiveTram(string tramNumber, bool repairStatus, bool cleanStatus, string? repairMessage, DepotDTO depot, ServiceType serviceType)
        {
            if (!IsTramAllreadyInDepot(tramNumber, depot, _sectorLogic, _tramlogic))
            {
                TramDTO tram = _tramlogic.GetTram(tramNumber);

                if (tram.DepotId == 1)
                {
                    changeTramStatus(tram, repairStatus, cleanStatus, _tramlogic);
                    if (repairStatus)
                    {
                        _repairServicelogic.CreateRepairLogDefect(tram, repairMessage, serviceType);
                    }
                    else
                    {
                        _repairServicelogic.DetermineRepairType(tram);
                    }

                    if (cleanStatus)
                    {
                        _cleaningServiceLogic.HasToBeCleaned(tram, ServiceType.Big);
                    }
                    else
                    {
                        _cleaningServiceLogic.DetermineCleaningType(tram);
                    }
                    AllocationManager.AllocateTramToTrack(tram, depot.DepotTracks, _tracklogic, _tramlogic, _repairServicelogic, _cleaningServiceLogic);
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

        public bool IsTramAllreadyInDepot(string tramNumber, DepotDTO depot, Sector sectorLogic, Tram tramLogic)
        {
            if (_tramlogic.IsTramAllreadyInDepot(tramNumber))
            {
                return true;
            }
            return false;
        }

        public int AmountOfRlTramsInDepot(DepotDTO depot, List<TramDTO> trams)
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
            StatusDTO statusInDepot = new StatusDTO {Status = TramStatus.Depot};
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

        public void TransferTram(string tramNumber, DepotDTO depot)
        {
            TramDTO tram = _tramlogic.GetTram(tramNumber);
            _sectorLogic.RemoveTram(_sectorLogic.GetSector(_sectorLogic.GetSectorByTramNumber(tramNumber)));
            bool cleanStatus = _tramAccess.GetCleanStatus(tramNumber);
            changeTramStatus(tram, true, cleanStatus, _tramlogic);
            AllocationManager.AllocateTramToTrack(tram, depot.DepotTracks, _tracklogic, _tramlogic, _repairServicelogic, _cleaningServiceLogic);
        }
    }
}
