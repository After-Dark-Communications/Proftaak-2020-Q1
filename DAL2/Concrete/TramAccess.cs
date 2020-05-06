using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DTO;
using Services;
using System.Data.SqlClient;
using DAL.Context;

namespace DAL.Concrete
{ 
    public class TramAccess : ITramAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;

        public TramAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(TramDTO obj)
        {
            string query = "INSERT INTO Tram (Type, TramNumber) VALUES (@type, @tramNumber)";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@type", (int)obj.Type);
                    command.Parameters.AddWithValue("@tramNumber", obj.TramNumber);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            if(obj.Status.Count > 0)
            {
                var tram = ReadFromTramNumber(obj.TramNumber);
                foreach (StatusDTO stat in obj.Status)
                {
                    AddStatus(tram.Id, stat);
                }
            }
        }

        public TramDTO Read(int key)
        {
            try
            {
                TramDTO returnTram = new TramDTO();
                string query = "SELECT * FROM Tram WHERE Id = @key";
                using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        con.Open();
                        command.Parameters.AddWithValue("@key", key);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                returnTram.Id = reader.GetInt32(0);
                                returnTram.Type = (TramType)reader.GetInt32(1);
                                returnTram.TramNumber = reader.GetString(2);
                            }
                        }
                        con.Close();
                    }
                }
                returnTram = CombineStatusWithTram(GetStatusesFromTram(returnTram.Id), returnTram);
                return returnTram;

            }
            catch
            {
                return null;
            }
            
        }

        public void Update(TramDTO obj) // now only updates statuses from the tram.
        {
        
            UpdateStatuses(obj, GetStatusesFromTram(obj.Id));
            
        }

        public void Delete(int key)
        {
            foreach(StatusDTO stat in GetStatusesFromTram(key))
            {
                DeleteStatus(stat.StatusId);
            }
            string query = "DELETE FROM Tram WHERE Id = @key";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@key", key);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public IEnumerable<TramDTO> GetAllTrams()
        {
            throw new NotImplementedException();
        }

        public int GetKeyFromTramNumber(string tramNumber)
        {
            int key = 0;
            string query = "Select dbo.Tram.Id From dbo.Tram Where TramNumber = @tramNumber";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@tramNumber", tramNumber);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                key = reader.GetInt32(0);
                            }
                                
                        }
                    }
                    con.Close();
                }
            }
            return key;
        }

        public int GetSectorIdFromTram(int key)
        {
            int sectorId = 0;
            string query = "Select dbo.Sector.Id From dbo.Sector Where dbo.Sector.TramId = @key";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@key", key);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                sectorId = reader.GetInt32(0);
                            }
                        }
                    }
                    con.Close();
                }
            }
            return sectorId;
        }

        public TramDTO ReadFromTramNumber(string tramNumber)
        {
            try
            {
                TramDTO returnTram  = new TramDTO();
                string query = "SELECT * FROM Tram WHERE TramNumber = @key";
                using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        con.Open();
                        command.Parameters.AddWithValue("@key", tramNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                returnTram.Id = reader.GetInt32(0);
                                returnTram.Type = (TramType)reader.GetInt32(1);
                                returnTram.TramNumber = reader.GetString(2);
                            }
                        }
                        con.Close();
                    }
                }
                returnTram = CombineStatusWithTram(GetStatusesFromTram(returnTram.Id), returnTram);

                return returnTram;
            }
            catch
            {
                return null;
            }
            
        }

        private void UpdateStatuses(TramDTO tram, List<StatusDTO> oldStatuses)
        {
            foreach(StatusDTO tramStat in tram.Status)
            {
                if(!oldStatuses.Any(s => s.StatusId == tramStat.StatusId))
                {
                    AddStatus(tram.Id, tramStat);
                }
            }
            foreach (StatusDTO stat in oldStatuses)
            {
                if(!tram.Status.Any(s => s.StatusId == stat.StatusId))
                {
                    DeleteStatus(stat.StatusId);
                }
            }
        }

        private void AddStatus(int tramKey, StatusDTO stat)
        {
            try
            {
                string query = "INSERT INTO Status_Tram (StatusId, TramId, Description) VALUES (@stat, @tramKey, @description)";
                using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        con.Open();
                        command.Parameters.AddWithValue("@stat", (int)stat.Status);
                        command.Parameters.AddWithValue("@tramKey", tramKey);
                        if (stat.Description != null)
                        {
                            command.Parameters.AddWithValue("@description", stat.Description);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@description", "");
                        }
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch
            {

            }
            
        }

        private void DeleteStatus(int key)
        {
            string query = "DELETE FROM Status_Tram WHERE Id = @key";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@key", key);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        private List<StatusDTO> GetStatusesFromTram(int key)
        {
            try
            {
                List<StatusDTO> returnList = new List<StatusDTO>();

                string query = "SELECT * FROM Status_Tram WHERE TramId = @key";
                using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        con.Open();
                        command.Parameters.AddWithValue("@key", key);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StatusDTO stat = new StatusDTO();
                                stat.Status = (TramStatus)reader.GetInt32(0);
                                stat.StatusId = reader.GetInt32(3);
                                stat.Description = reader.GetString(2);
                                returnList.Add(stat);
                            }
                        }
                        con.Close();
                    }
                }

                return returnList;
            }
            catch
            {
                return null;
            }
            
        }

        private TramDTO CombineStatusWithTram(List<StatusDTO> stats, TramDTO tram)
        {
            if(stats != null)
            {
                tram.Status = stats;
            }
            return tram;
        }

        public List<int> GetAllTramIds()
        {
            List<int> returnList = new List<int>();

            string query = "SELECT * FROM Tram";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnList.Add(reader.GetInt32(0));
                        }
                    }
                    con.Close();
                }
            }

            return returnList;
        }
    }
}
