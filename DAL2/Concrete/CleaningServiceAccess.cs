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
                using (SqlCommand cmd = new SqlCommand("INSERT INTO CleaningService_Tram (CleaningServiceId, TramId, CleanType, Occured) " +
                                                       "VALUES((select CleaningService.Id FROM CleaningService WHERE CleaningService.Location = @Location), " +
                                                       "(select Tram.Id FROM Tram Where Tram.TramNumber = @TramNumber), " +
                                                       "@CleanType, @Occured)" , conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Location", cleanLog.CleaningService.Location));
                    cmd.Parameters.Add(new SqlParameter("@TramNumber", cleanLog.Tram.TramNumber));
                    cmd.Parameters.Add(new SqlParameter("@CleanType", cleanLog.ServiceType));
                    cmd.Parameters.Add(new SqlParameter("@Occured", cleanLog.Occured));
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

        public void UpdateSchedulingCleanLog(CleaningLogDTO cleanLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE CleaningService_Tram SET CleaningDate = @CleaningDate, UserId = @UserId WHERE CleaningId = @CleaningId", conn))
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
            string Name = "";
            DateTime date = default;
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Select CleaningService_Tram.CleaningId, CleaningService.Location, Tram.TramNumber, CleaningService_Tram.CleaningDate, CleaningService_Tram.Occured, CleaningService_Tram.CleanType, [User].Name " +
                                                       "FROM CleaningService_Tram " +
                                                       "INNER JOIN CleaningService ON CleaningService_Tram.CleaningServiceId = CleaningService.Id " +
                                                       "LEFT JOIN [User] ON CleaningService_Tram.UserId = [User].Id " +
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
                                Name = dataReader.GetString(6);
                            }
                            CleaningLogDTO cleanLog = new CleaningLogDTO(id, new CleaningServiceDTO(location), new TramDTO(tramnumber), new UserDTO(Name), date, ServiceType, Occured);
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
            string Name = "";
            DateTime CleaningDate = default;
            List<CleaningLogDTO> cleanLogList = new List<CleaningLogDTO>();
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select CleaningService.Location, Tram.TramNumber, CleaningService_Tram.CleaningDate, CleaningService_Tram.Occured, CleaningService_Tram.CleaningType, [User].Name " +
                                                       "FROM CleaningService_Tram " +
                                                       "INNER JOIN CleaningService ON CleaningService_Tram.CleaningServiceId = CleaningService.Id " +
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
                                Name = dataReader.GetString(6);
                            }
                            CleaningLogDTO cleanLog = new CleaningLogDTO(id, new CleaningServiceDTO(), new TramDTO(tramnumber), new UserDTO(Name), CleaningDate, ServiceType, Occured);
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

        public void DeleteNotOccured(bool Occured)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM CleaningService_Tram Where Occured= @Occured", conn))
                {
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@Occured", Occured);
                    conn.Close();
                }
            }
        }
    }
}
