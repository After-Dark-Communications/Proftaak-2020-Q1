using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL.Concrete
{
    public class CleaningServiceAccess : ICleaningServiceAccess
    {
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

        //public List<CleaningServiceDTO> Logs()
        //{
        //    using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("Select ", conn))
        //        {

        //        }
        //    }
        //}
        public void StoreCleaningLog(CleaningLogDTO cleanLog)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO CleaningService_Tram (CleaningServiceId, RepairDate, TramId, ServiceType, Occured, UserId, RepairMessage) " +
                    "VALUES((select CleaningService.Id FROM CleaningService WHERE CleaningService.Location = @Location)," +
                    "@Date," +
                    "(select Tram.Id FROM Tram Where Tram.TramNumber = @TramNumber)," +
                    "@ServiceType," +
                    "@Occured, " +
                    "(select [User].Id FROM [User] WHERE [User].Name = @UserName), @RepairMessage)", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Location", cleanLog.CleaningService.Location));
                    cmd.Parameters.Add(new SqlParameter("@Date", cleanLog.RepairDate));
                    cmd.Parameters.Add(new SqlParameter("@TramNumberm", cleanLog.Tram.TramNumber));
                    cmd.Parameters.Add(new SqlParameter("@ServiceType", cleanLog.ServiceType));
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

                using (SqlCommand cmd = new SqlCommand("", conn))
                {

                    cmd.Parameters.Add(new SqlParameter("@UserName", cleanLog.User.UserName));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
        public IEnumerable<CleaningLogDTO> GetCleaningLogs()
        {
            List<CleaningLogDTO> cleanLogList = new List<CleaningLogDTO>();
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
                            CleaningLogDTO cleanLog = new CleaningLogDTO(new CleaningServiceDTO(), new TramDTO(tramnumber), new UserDTO(Name), date, ServiceType, Occured, RepairMessage);
                            cleanLogList.Add(cleanLog);
                        }
                    }
                }
                conn.Close();
            }
            return cleanLogList;
        }
    }
}
