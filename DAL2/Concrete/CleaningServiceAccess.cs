using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using Microsoft.Data.SqlClient;
using Services;

namespace DAL.Concrete
{
    public class CleaningServiceAccess : ICleaningServiceAccess
    {
        public void UpdateCleaningService(CleaningServiceDTO cleaningService)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UPDATE CleaningService SET SmallCleansPerDay = @SmallClean, BigCleansPerDay = @BigClean WHERE CleaningService.Location = @Location", conn))
                {
                    cmd.Parameters.AddWithValue("@SmallClean", cleaningService.MaxSmallServicePerDay);
                    cmd.Parameters.AddWithValue("@BigClean", cleaningService.MaxBigServicePerDay);
                    cmd.Parameters.AddWithValue("@Location", cleaningService.Location);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void StoreCleaningLog(CleaningLogDTO cleanLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO CleaningService_Tram (CleaningServiceId, CleaningDate, TramId, CleanType, Occured, UserId) " +
                    "VALUES((select CleaningService.Id FROM CleaningService WHERE CleaningService.Location = @Location)," +
                    "@Date," +
                    "(select Tram.Id FROM Tram Where Tram.TramNumber = @TramNumber)," +
                    "@CleanType," +
                    "@Occured, " +
                    "(select [User].Id FROM [User] WHERE [User].Name = @UserName))", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Location", cleanLog.CleaningService.Location));
                    cmd.Parameters.Add(new SqlParameter("@Date", cleanLog.RepairDate));
                    cmd.Parameters.Add(new SqlParameter("@TramNumber", cleanLog.Tram.TramNumber));
                    cmd.Parameters.Add(new SqlParameter("@CleanType", cleanLog.ServiceType));
                    cmd.Parameters.Add(new SqlParameter("@Occured", cleanLog.Occured));
                    cmd.Parameters.Add(new SqlParameter("@UserName", cleanLog.User.UserName ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("RepairMessage", cleanLog.RepairMessage ?? (object)DBNull.Value));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void UpdateCleanLog(CleaningLogDTO cleanLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE CleaningService_Tram SET Occured = @Occured WHERE CleaningId = @CleaningId", conn))
                {
                    cmd.Parameters.AddWithValue("@Occured", cleanLog.Occured);
                    cmd.Parameters.AddWithValue("@CleaningId", cleanLog.Id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public CleaningServiceDTO GetCleaningServiceByLocation(string Location)
        {
            CleaningServiceDTO cleaningService = new CleaningServiceDTO();
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM CleaningService WHERE Location = @Location", conn))
                {
                    cmd.Parameters.AddWithValue("@Location", Location);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            cleaningService.Id = dataReader.GetInt32(0);
                            cleaningService.MaxSmallServicePerDay = dataReader.GetInt32(1);
                            cleaningService.MaxBigServicePerDay = dataReader.GetInt32(2);
                            cleaningService.Location = dataReader.GetString(3);
                        }
                    }
                }
                conn.Close();
            }
            return cleaningService;
        }

        public IEnumerable<CleaningLogDTO> GetCleaningLogs()
        {
            List<CleaningLogDTO> cleanLogList = new List<CleaningLogDTO>();
            string RepairMessage = "";
            string Name = "";
            DateTime date = default;
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Select RepairService.Location, Tram.TramNumber, RepairService_Tram.RepairDate, RepairService_Tram.Occured, RepairService_Tram.ServiceType , RepairService_Tram.RepairMessage, [User].Name " +
                                                       "FROM RepairService_Tram " +
                                                       "INNER JOIN CleaningService ON CleaningService_Tram.CleaningServiceId = CleaningService.Id " +
                                                       "Left JOIN [User] ON CleaningService_Tram.UserId = [User].Id " +
                                                       "INNER JOIN Tram ON CleaningService_Tram.TramId = Tram.Id ", conn))
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
                            CleaningLogDTO cleanLog = new CleaningLogDTO(id, new CleaningServiceDTO(), new TramDTO(tramnumber), new UserDTO(Name), date, ServiceType, Occured, RepairMessage);
                            cleanLogList.Add(cleanLog);
                        }
                    }
                }
                conn.Close();
            }
            return cleanLogList;
        }
        public IEnumerable<CleaningLogDTO> GetCleaningLogsByTramNumber(string tramnumber)
        {
            string RepairMessage = "";
            string Name = "";
            DateTime CleaningDate = default;
            List<CleaningLogDTO> cleanLogList = new List<CleaningLogDTO>();
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select RepairService.Location, Tram.TramNumber, RepairService_Tram.RepairDate, RepairService_Tram.Occured, RepairService_Tram.ServiceType , RepairService_Tram.RepairMessage, [User].Name " +
                                                       "FROM RepairService_Tram " +
                                                       "INNER JOIN RepairService ON RepairService_Tram.RepairServiceId = RepairService.Id " +
                                                       "INNER JOIN [User] ON RepairService_Tram.UserId = [User].Id " +
                                                       "INNER JOIN Tram ON RepairService_Tram.TramId = Tram.Id " +
                                                       "WHERE Tram.TramNumber ", conn))
                {
                    cmd.Parameters.AddWithValue("TramNumber", tramnumber);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            int id = dataReader.GetInt32(0);
                            string location = dataReader.GetString(1);
                            if (!dataReader.IsDBNull(3))
                            {
                                CleaningDate = dataReader.GetDateTime(3);
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
                            CleaningLogDTO cleanLog = new CleaningLogDTO(id, new CleaningServiceDTO(), new TramDTO(tramnumber), new UserDTO(Name), CleaningDate, ServiceType, Occured, RepairMessage);
                            cleanLogList.Add(cleanLog);
                        }
                    }
                }
                conn.Close();
            }
            return cleanLogList;
        }

        public void Create(CleaningServiceDTO obj)
        {
            throw new NotImplementedException();
        }

        public CleaningServiceDTO Read(int key)
        {
            throw new NotImplementedException();
        }

        public void Update(CleaningServiceDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
