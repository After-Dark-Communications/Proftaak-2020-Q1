using AutoMapper;
using Context;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using DAL.Context;
namespace DAL.Concrete
{
    public class RepairServiceAccess
    {
        public void GetAllTracks(RepairLogDTO repairLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.Track.TrackNumber FROM dbo.Depot INNER JOIN dbo.Track ON dbo.Depot.Id = dbo.Track.DepotId;"))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                           
                        }
                    }
                }
            }
        }
    }
}
