using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using DAL.Interfaces;
using DTO;
using AutoMapper;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using DAL.Context;
using System.ComponentModel;
using DAL.Models;
using Services;

namespace DAL.Concrete
{
    public class SectorAccess : ISectorAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;
        public SectorAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(SectorDTO obj)
        {
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                throw new NotImplementedException();
            }
        }

        public void Delete(int key)
        {
            throw new NotImplementedException();
        }

        public SectorDTO Read(int key)
        {
            SectorDTO sector = new SectorDTO();
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand($"SELECT dbo.Sector.Id, dbo.Sector.TramId, dbo.Sector.Location, dbo.Sector.SectorType, Track.TrackNumber FROM dbo.Sector INNER JOIN dbo.Track ON dbo.Sector.TrackId = dbo.Track.Id INNER JOIN dbo.Tram ON dbo.Sector.Id = dbo.Tram.Id WHERE dbo.Sector.Id = @key", conn))
                {
                    cmd.Parameters.AddWithValue("@key", key);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {   
                        while(dataReader.Read())
                        {
                            sector.Id = dataReader.GetInt32(0);
                            
                            if(!dataReader.IsDBNull(1))
                            {
                                sector.Tram = new TramDTO();
                                sector.Tram.Id = dataReader.GetInt32(1);
                            }
                            sector.Id = dataReader.GetInt32(0);
                            sector.SectorPosition = dataReader.GetInt32(2);
                            sector.SectorType = (SectorType)dataReader.GetInt32(3);
                            sector.TrackNumber = dataReader.GetInt32(4);
                        }
                    }
                }
                conn.Close();
            }
            return sector;
        }

        public void Update(SectorDTO obj)
        {
            try
            {
                if (obj.Tram != null)
                {
                    using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Sector SET TramId = @tramId WHERE Id = @sectorId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@sectorId", obj.Id);
                            cmd.Parameters.AddWithValue("@tramId", obj.Tram.Id);
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
                else
                {
                    RemoveTram(obj);
                }
            }
            catch
            {

            }
            
        }

        private void RemoveTram(SectorDTO sector)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                string query = "UPDATE Sector SET TramId = null WHERE Id = @sectorId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sectorId", sector.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public List<SectorDTO> GetAllTracks()
        {
            List<SectorDTO> returnList = new List<SectorDTO>();


            return null;
        }
    }
}
