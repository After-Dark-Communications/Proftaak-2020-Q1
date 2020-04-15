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
        public async Task Create(TramDTO obj)
        {
            using (_context)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int key)
        {
            
        }

        public IEnumerable<TramDTO> GetAllTrams()
        {
            throw new NotImplementedException();
        }

        public TramDTO ReadFromTramNumber(string tramNumber)
        {
            TramDTO returnTram = new TramDTO();
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

            return returnTram;
        }

        public TramDTO Read(int key)
        {
            TramDTO returnTram = new TramDTO();
            string query = "SELECT * FROM Tram WHERE Id = @key";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@key", key);
                    using(SqlDataReader reader = command.ExecuteReader())
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
            returnTram.Status = GetStatusesFromTram(key);
            return returnTram;
        }


        public async Task Update(TramDTO obj) // now only updates statuses from the tram.
        {
            UpdateStatuses(obj, GetStatusesFromTram(obj.Id));
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
            string query = "INSERT INTO Status_Tram (StatusId, TramId, Description) VALUES (@stat, @tramKey, @description)";
            using (SqlConnection con = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@stat", (int)stat.Status);
                    command.Parameters.AddWithValue("@tramKey", tramKey);
                    command.Parameters.AddWithValue("@description", stat.Description);
                    command.ExecuteNonQuery();
                    con.Close();
                }
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
            List<StatusDTO> returnList = new List<StatusDTO>();

            string query ="SELECT * FROM Status_Tram WHERE TramId = @key";
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
    }
}
