using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DTO;

namespace DAL.Interfaces
{
    interface ITramAccess : IGenAccess<TramDTO>
    {
        IEnumerable<TramDTO> GetAllTrams();
    }
}
