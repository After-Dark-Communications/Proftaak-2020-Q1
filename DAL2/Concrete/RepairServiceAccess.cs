using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
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

                using (SqlCommand cmd = new SqlCommand("INSERT INTO RepairService_Tram (RepairServiceId, RepairDate,TramId, ServiceType, Occured, UserId, RepairMessage) " +
                                                       "VALUES((select RepairService.Id FROM RepairService WHERE RepairService.Location = @Location)," +
                                                       "@Date, " +
                                                       "(select Tram.Id FROM Tram WHERE Tram.TramNumber = @TramNumber), " +
                                                       "@ServiceType," +
                                                       " @Occured, " +
                                                       "(select [User].Id FROM [User] WHERE [User].Name = @UserName), @RepairMessage) ", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Location", repairLog.RepairService.Location));
                    cmd.Parameters.Add(new SqlParameter("@Date", repairLog.RepairDate));
                    cmd.Parameters.Add(new SqlParameter("@TramNumber", repairLog.Tram.TramNumber));
                    cmd.Parameters.Add(new SqlParameter("@ServiceType", repairLog.ServiceType));
                    cmd.Parameters.Add(new SqlParameter("@Occured", repairLog.Occured));
                    cmd.Parameters.Add(new SqlParameter("@UserName", repairLog.User.UserName ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@RepairMessage", repairLog.RepairMessage ?? (object)DBNull.Value));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void UpdateRepairLog(RepairLogDTO repairLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                   
                    cmd.Parameters.Add(new SqlParameter("@UserName", repairLog.User.UserName));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public IEnumerable<RepairLogDTO> GetRepairLogs()
        {
            List<RepairLogDTO> repairLogList = new List<RepairLogDTO>();
            string RepairMessage = "";
            string Name = "";
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Select RepairService.Location, Tram.TramNumber, RepairService_Tram.RepairDate, RepairService_Tram.Occured, RepairService_Tram.ServiceType , RepairService_Tram.RepairMessage, [User].Name " +
                                                       "FROM RepairService_Tram " +
                                                       "INNER JOIN RepairService ON RepairService_Tram.RepairServiceId = RepairService.Id " +
                                                       "INNER JOIN [User] ON RepairService_Tram.UserId = [User].Id " +
                                                       "INNER JOIN Tram ON RepairService_Tram.TramId = Tram.Id ", conn))
                {

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            string location = dataReader.GetString(0);
                            string tramnumber = dataReader.GetString(1);
                            DateTime date = dataReader.GetDateTime(2);
                            Boolean Occured = dataReader.GetBoolean(3);
                            int ServiceType = dataReader.GetInt32(4);
                            if (!dataReader.IsDBNull(5))
                            {
                                RepairMessage = dataReader.GetString(5);
                            }
                            if (!dataReader.IsDBNull(6))
                            {
                                Name = dataReader.GetString(6);
                            }
                            RepairLogDTO repairLog = new RepairLogDTO(new RepairServiceDTO(location), new TramDTO(tramnumber), new UserDTO(Name), date, ServiceType, Occured, RepairMessage);
                            repairLogList.Add(repairLog);
                        }
                    }
                }
                conn.Close();
            }
            return repairLogList;
        }
    }
}
