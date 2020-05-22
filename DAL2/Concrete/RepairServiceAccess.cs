using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL.Context;
using Services;
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

                using (SqlCommand cmd = new SqlCommand("UPDATE RepairService_Tram SET RepairDate = @RepairDate, Occured = @Occured, UserId = @UserId WHERE RepairId = @RepairId", conn))
                {
                    cmd.Parameters.AddWithValue("@RepairDate", repairLog.RepairDate);
                    cmd.Parameters.AddWithValue("@Occured", repairLog.Occured);
                    cmd.Parameters.AddWithValue("@UserId", repairLog.User.Id);
                    cmd.Parameters.AddWithValue("@RepairId", repairLog.Id);
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

                using (SqlCommand cmd = new SqlCommand("Select RepairService_Tram.RepairId, RepairService.Location, Tram.TramNumber, RepairService_Tram.RepairDate, RepairService_Tram.Occured, RepairService_Tram.ServiceType , RepairService_Tram.RepairMessage, [User].Name " +
                                                       "FROM RepairService_Tram " +
                                                       "INNER JOIN RepairService ON RepairService_Tram.RepairServiceId = RepairService.Id " +
                                                       "INNER JOIN [User] ON RepairService_Tram.UserId = [User].Id " +
                                                       "INNER JOIN Tram ON RepairService_Tram.TramId = Tram.Id ", conn))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int id = dataReader.GetInt32(0);
                            string location = dataReader.GetString(1);
                            string tramnumber = dataReader.GetString(2);
                            DateTime date = dataReader.GetDateTime(3);
                            Boolean Occured = dataReader.GetBoolean(4);
                            ServiceType ServiceType = (ServiceType)dataReader.GetInt32(5);
                            if (!dataReader.IsDBNull(6))
                            {
                                RepairMessage = dataReader.GetString(6);
                            }
                            if (!dataReader.IsDBNull(7))
                            {
                                Name = dataReader.GetString(7);
                            }
                            RepairLogDTO repairLog = new RepairLogDTO( id, new RepairServiceDTO(location), new TramDTO(tramnumber), new UserDTO(Name), date, ServiceType, Occured, RepairMessage);
                            repairLogList.Add(repairLog);
                        }
                    }
                }
                conn.Close();
            }
            return repairLogList;
        }

        public IEnumerable<RepairLogDTO> GetRepairLogsByTramNumber(string tramnumber)
        {
            List<RepairLogDTO> repairLogList = new List<RepairLogDTO>();
            string RepairMessage = "";
            string Name = "";
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Select RepairService_Tram.RepairId, RepairService.Location, Tram.TramNumber, RepairService_Tram.RepairDate, RepairService_Tram.Occured, RepairService_Tram.ServiceType , RepairService_Tram.RepairMessage, [User].Name " +
                                                       "FROM RepairService_Tram " +
                                                       "INNER JOIN RepairService ON RepairService_Tram.RepairServiceId = RepairService.Id " +
                                                       "INNER JOIN [User] ON RepairService_Tram.UserId = [User].Id " +
                                                       "INNER JOIN Tram ON RepairService_Tram.TramId = Tram.Id " +
                                                       "WHERE Tram.TramNumber ", conn))
                {
                    cmd.Parameters.AddWithValue("TramNumber", tramnumber);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int id = dataReader.GetInt32(0);
                            string location = dataReader.GetString(1);
                            string dbtramnumber = dataReader.GetString(2);
                            DateTime date = dataReader.GetDateTime(3);
                            Boolean Occured = dataReader.GetBoolean(4);
                            ServiceType ServiceType = (ServiceType)dataReader.GetInt32(5);
                            if (!dataReader.IsDBNull(6))
                            {
                                RepairMessage = dataReader.GetString(6);
                            }
                            if (!dataReader.IsDBNull(7))
                            {
                                Name = dataReader.GetString(7);
                            }
                            RepairLogDTO repairLog = new RepairLogDTO(id, new RepairServiceDTO(location), new TramDTO(dbtramnumber), new UserDTO(Name), date, ServiceType, Occured, RepairMessage);
                            repairLogList.Add(repairLog);
                        }
                    }
                }
                conn.Close();
            }
            return repairLogList;
        }
        public RepairServiceDTO GetRepairServiceByLocation(string Location)
        {
            RepairServiceDTO repairService = new RepairServiceDTO();
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("SELECT * FROM RepairService WHERE Location = @Location"))
                {
                    cmd.Parameters.AddWithValue("@Location", Location);
                    using(SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            repairService.Id = dataReader.GetInt32(0);
                            repairService.MaxSmallServicePerDay = dataReader.GetInt32(1);
                            repairService.MaxBigServicePerDay = dataReader.GetInt32(2);
                            repairService.Location = dataReader.GetString(3);
                        }
                    }
                }
                conn.Close();
            }
            return repairService;
        }
    }
}
