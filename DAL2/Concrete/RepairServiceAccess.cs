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
    public class RepairServiceAccess : IRepairServiceAccess
    {
        public void StoreRepairLog(RepairLogDTO repairLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO RepairService_Tram (RepairServiceId, RepairDate,TramId, ServiceType, Occured, UserId) " +
                                                       "VALUES((select RepairService.Id FROM RepairService WHERE RepairService.Location = @Location)," +
                                                       "@Date, " +
                                                       "(select Tram.Id FROM Tram WHERE Tram.TramNumber = @TramNumber), " +
                                                       "@ServiceType," +
                                                       " @Occured, " +
                                                       "(select [User].Id FROM [User] WHERE [User].Name = @UserName)) ", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Location", repairLog.RepairService.Location));
                    cmd.Parameters.Add(new SqlParameter("@Date", repairLog.RepairDate));
                    cmd.Parameters.Add(new SqlParameter("@TramNumber", repairLog.Tram.TramNumber));
                    cmd.Parameters.Add(new SqlParameter("@ServiceType", repairLog.ServiceType));
                    cmd.Parameters.Add(new SqlParameter("@Occured", repairLog.Occured));
                    cmd.Parameters.Add(new SqlParameter("@UserName", repairLog.User.UserName));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
