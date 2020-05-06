using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using DAL.Context;
using DAL.Models;

namespace DAL.Concrete
{
    public class ServiceAccess
    {
        public RepairServiceDTO Read()
        {
            RepairServiceDTO RepairService = new RepairServiceDTO();
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT dbo.RepairService.Id, dbo.RepairService_Tram.UserId, dbo.RepairService_Tram.Occured, dbo.RepairService_Tram.RepairDate, dbo.RepairService.BigRepairsPerDay, dbo.RepairService.SmallRepairsPerDay, dbo.RepairService_Tram.ServiceType, dbo.RepairService_Tram.RepairId FROM dbo.RepairService INNER JOIN dbo.RepairService_Tram ON dbo.RepairService.Id = dbo.RepairService_Tram.RepairServiceId;"))
                {
                    conn.Open();
                    using (SqlDataReader datareader = cmd.ExecuteReader())
                    {
                        while (datareader.Read())
                        {
                            RepairService.Id = datareader.GetInt32(0);
                            RepairService.UserId = datareader.GetInt32(1);
                            RepairService.Occured = datareader.GetBoolean(2);
                            RepairService.RepairDate = DateTime.Parse(datareader.GetString(3));
                            RepairService.MaxBigServicePerDay = datareader.GetInt32(4);
                            RepairService.MaxSmallServicePerDay = datareader.GetInt32(5);
                            RepairService.ServiceType = datareader.GetInt32(6);
                        }
                    }
                    conn.Close();
                }
                return RepairService;
            }
        }
    }
}
