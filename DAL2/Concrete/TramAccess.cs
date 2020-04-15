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
            using (_context)
            {
                var tram = _context.Tram.FirstOrDefaultAsync(t => t.Id == key);
                _context.Remove(tram);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<TramDTO> GetAllTrams()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<StatusDTO> GetAllStatuses(int key)
        {

            throw new NotImplementedException();
        }

        public TramDTO ReadFromTramNumber(string tramNumber)
        {
            TramDTO returnTram = new TramDTO();
            string query = "SELECT Status_Tram.Id, Status_Tram.StatusId, Status_Tram.Description, Status_Tram.TramId, Tram.TramNumber, Tram.Type FROM Tram INNER JOIN Status_Tram on Tram.Id = Status_Tram.TramId WHERE  TramNumber= @key";
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
                            returnTram.Id = reader.GetInt32(3);
                            returnTram.Type = (TramType)reader.GetInt32(5);
                            returnTram.TramNumber = reader.GetString(4);
                            StatusDTO stat = new StatusDTO();
                            stat.Status = (TramStatus)reader.GetInt32(1);
                            stat.StatusId = reader.GetInt32(0);
                            stat.Description = reader.GetString(2);
                            returnTram.Status.Add(stat);
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
            string query = "SELECT Status_Tram.Id, Status_Tram.StatusId, Status_Tram.Description, Status_Tram.TramId, Tram.TramNumber, Tram.Type " +
                "FROM Status_Tram INNER JOIN Tram on Status_Tram.TramId = Tram.Id " +
                "WHERE TramId = @key";
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
                            returnTram.Id = reader.GetInt32(3);
                            returnTram.Type = (TramType)reader.GetInt32(5);
                            returnTram.TramNumber = reader.GetString(4);
                            StatusDTO stat = new StatusDTO();
                            stat.Status = (TramStatus)reader.GetInt32(1);
                            stat.StatusId = reader.GetInt32(0);
                            stat.Description = reader.GetString(2);
                            returnTram.Status.Add(stat);
                        }
                    }
                    con.Close();
                }
            }

            return returnTram;
        }


        public async Task Update(TramDTO obj)
        {
            string query = "UPDATE Status_Tram";
        }
    }
}
