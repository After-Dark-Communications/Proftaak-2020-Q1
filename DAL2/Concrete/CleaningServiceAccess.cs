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

        public List<CleaningServiceDTO> Logs()
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select ", conn))
                {

                }
            }
        }
    }
}
