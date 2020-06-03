using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using DAL.Context;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Concrete
{
    public class ServiceAccess : IServiceAccess
    {
        public RepairServiceDTO ReadRepair()
        {
            RepairServiceDTO RepairService = new RepairServiceDTO("RMS");
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT dbo.RepairService.Id, dbo.RepairService_Tram.UserId, dbo.RepairService_Tram.Occured, dbo.RepairService_Tram.Date, dbo.RepairService.BigRepairsPerDay, dbo.RepairService.SmallRepairsPerDay, dbo.RepairService_Tram.ServiceType, dbo.RepairService_Tram.RepairId, dbo.RepairService.Location FROM dbo.RepairService INNER JOIN dbo.RepairService_Tram ON dbo.RepairService.Id = dbo.RepairService_Tram.RepairServiceId;", conn))
                {
                    conn.Open();
                    using (SqlDataReader datareader = cmd.ExecuteReader())
                    {
                        while (datareader.Read())
                        {
                            RepairService.Id = datareader.GetInt32(0);
                            //RepairService.UserId = datareader.GetInt32(1);
                            //RepairService.Date = datareader.GetDateTime(3);
                            RepairService.MaxBigServicePerDay = datareader.GetInt32(4);
                            RepairService.MaxSmallServicePerDay = datareader.GetInt32(5);
                        }
                    }
                    conn.Close();
                }
                return RepairService;
            }
        }
        public CleaningServiceDTO ReadCleaning()
        {
            CleaningServiceDTO CleaningService = new CleaningServiceDTO();
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using(SqlCommand cmd  = new SqlCommand($"SELECT dbo.CleaningService.Id, dbo.CleaningService_Tram.UserId, dbo.CleaningService_Tram.Occured, dbo.CleaningService_Tram.CleaningDate, dbo.CleaningService.BigCleansPerDay, dbo.CleaningService.SmallCleansPerDay, dbo.CleaningService_Tram.CleanType, dbo.CleaningService_Tram.CleaningServiceId FROM dbo.CleaningService INNER JOIN dbo.CleaningService_Tram ON dbo.CleaningService.Id = dbo.CleaningService_Tram.CleaningServiceId", conn))
                {
                    conn.Open();
                    using(SqlDataReader datareader = cmd.ExecuteReader())
                    {
                        while (datareader.Read())
                        {

                            CleaningService.Id = datareader.GetInt32(0);
                            CleaningService.UserId = datareader.GetInt32(1);
                            CleaningService.Occured = datareader.GetBoolean(2);
                            CleaningService.CleanDate = DateTime.Parse(datareader.GetString(3));
                            CleaningService.MaxBigServicePerDay = datareader.GetInt32(4);
                            CleaningService.MaxSmallServicePerDay = datareader.GetInt32(5);
                            CleaningService.ServiceType = datareader.GetInt32(6);
                        }
                    }
                    conn.Close();
                }
            }
            return CleaningService;
        }
        public void UpdateRepair()
        {
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
            }
        }
        public void UpdateCleaning()
        {
            using(SqlConnection conn= new SqlConnection(DBConnection._connectionString))
            {
                using(SqlCommand cmd = new SqlCommand($"UPDATE dbo.CleaningService"))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
