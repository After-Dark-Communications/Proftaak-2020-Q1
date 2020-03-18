using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    interface ITramAccess : IGenAccess<TramDal>
    {
        IEnumerable<TramDal> GetAllTrams();
    }
}
