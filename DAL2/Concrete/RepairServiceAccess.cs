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
        public void UpdateRepairService(RepairServiceDTO repairService)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE RepairService SET SmallRepairsPerDay = @SmallRepairs, BigRepairsPerDay = @BigRepairs WHERE RepairService.Location = @Location", conn))
                {
                    cmd.Parameters.AddWithValue("@SmallRepairs", repairService.MaxSmallServicePerDay);
                    cmd.Parameters.AddWithValue("@BigRepairs", repairService.MaxBigServicePerDay);
                    cmd.Parameters.AddWithValue("@Location", repairService.Location);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void StoreRepairLog(RepairLogDTO repairLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO RepairService_Tram (RepairServiceId, TramId, ServiceType, Occured, RepairMessage, WaitingList) " +
                                                       "VALUES((select RepairService.Id FROM RepairService WHERE RepairService.Location = @Location), " +
                                                       "(select Tram.Id FROM Tram WHERE Tram.TramNumber = @TramNumber), " +
                                                       "@ServiceType, " +
                                                       "@Occured, " +
                                                       "@RepairMessage, @WaitingList) ", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Location", repairLog.RepairService.Location));
                    cmd.Parameters.Add(new SqlParameter("@TramNumber", repairLog.Tram.TramNumber));
                    cmd.Parameters.Add(new SqlParameter("@ServiceType", repairLog.ServiceType));
                    cmd.Parameters.Add(new SqlParameter("@Occured", repairLog.Occured));
                    cmd.Parameters.Add(new SqlParameter("@RepairMessage", repairLog.RepairMessage ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@WaitingList", repairLog.WaitingList));
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

                using (SqlCommand cmd = new SqlCommand("UPDATE RepairService_Tram SET Date = @Date, Occured = @Occured, UserId = @UserId WHERE RepairId = @RepairId", conn))
                {
                    cmd.Parameters.AddWithValue("@Date", repairLog.RepairDate);
                    cmd.Parameters.AddWithValue("@Occured", repairLog.Occured);
                    cmd.Parameters.AddWithValue("@UserId", repairLog.User.Id);
                    cmd.Parameters.AddWithValue("@RepairId", repairLog.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void UpdateWaitingList(RepairLogDTO repairLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE RepairService_Tram SET Waitinglist = @WaitingList, WHERE RepairId = @RepairId", conn))
                {
                    cmd.Parameters.AddWithValue("@WaitingList", repairLog.WaitingList);
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
            DateTime date = default;
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Select RepairService_Tram.RepairId, RepairService.Location, Tram.TramNumber, RepairService_Tram.Date, RepairService_Tram.Occured, RepairService_Tram.ServiceType , RepairService_Tram.RepairMessage, [User].Name, RepairService_Tram.Waitinglist " +
                                                       "FROM RepairService_Tram " +
                                                       "INNER JOIN RepairService ON RepairService_Tram.RepairServiceId = RepairService.Id " +
                                                       "LEFT JOIN [User] ON RepairService_Tram.UserId = [User].Id " +
                                                       "INNER JOIN Tram ON RepairService_Tram.TramId = Tram.Id ", conn))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int id = dataReader.GetInt32(0);
                            string location = dataReader.GetString(1);
                            string tramnumber = dataReader.GetString(2);
                            if (!dataReader.IsDBNull(3))
                            {
                                date = dataReader.GetDateTime(3);
                            }
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
                            bool WaitingList = dataReader.GetBoolean(8);

                            RepairLogDTO repairLog = new RepairLogDTO(id, new RepairServiceDTO(location), new TramDTO(tramnumber), date, new UserDTO(Name), ServiceType, Occured, RepairMessage, WaitingList);
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
            DateTime RepairDate = default;
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Select RepairService_Tram.RepairId, RepairService.Location, Tram.TramNumber, RepairService_Tram.Occured, RepairService_Tram.ServiceType , RepairService_Tram.RepairMessage, RepairService_Tram.Date " +
                                                       "FROM RepairService_Tram " +
                                                       "INNER JOIN RepairService ON RepairService_Tram.RepairServiceId = RepairService.Id " +
                                                       "INNER JOIN Tram ON RepairService_Tram.TramId = Tram.Id " +
                                                       "WHERE Tram.TramNumber = @tramNumber ", conn))
                {
                    cmd.Parameters.AddWithValue("@TramNumber", tramnumber);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int id = dataReader.GetInt32(0);
                            string location = dataReader.GetString(1);
                            string dbtramnumber = dataReader.GetString(2);
                            bool Occured = dataReader.GetBoolean(3);
                            ServiceType ServiceType = (ServiceType)dataReader.GetInt32(4);
                            if (!dataReader.IsDBNull(5))
                            {
                                RepairMessage = dataReader.GetString(5);
                            }
                            if (!dataReader.IsDBNull(6))
                            {
                                RepairDate = dataReader.GetDateTime(6);
                            }
                            RepairLogDTO repairLog = new RepairLogDTO(id, new RepairServiceDTO(location), new TramDTO(dbtramnumber), RepairDate, ServiceType, Occured, RepairMessage);
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
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM RepairService WHERE Location = @Location", conn))
                {
                    cmd.Parameters.AddWithValue("@Location", Location);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
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

        public void DeleteNotOccured()
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM RepairService_Tram Where Occured= 0"))
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
