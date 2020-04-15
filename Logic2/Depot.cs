using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL.Interfaces;

namespace Logic
{
    public class Depot
    {
        private readonly IDepotAccess _depotaccess;
        public Depot(IDepotAccess depotAccess)
        {
            _depotaccess = depotAccess;
        }
        private bool CheckIfTramIsAllowed(TramDTO tram)
        {
            throw new NotImplementedException();
        }

        public void ReceiveTram(TramDTO tram) 
        {
            if (CheckIfTramIsAllowed(tram))
            {
                AllocationManager.AllocateTramToTrack(tram);
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
