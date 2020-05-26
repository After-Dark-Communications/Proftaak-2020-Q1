using AutoMapper;
using Context;
using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class TrackAccess : ITrackAccess
    {
        private readonly IMapper _mapper;

        public TrackAccess(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Create(TrackDTO obj)
        {
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                throw new NotImplementedException();
            }
        }
        public TrackDTO Read(int key)
        {
            TrackDTO track = new TrackDTO();
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using(SqlCommand cmd = new SqlCommand($"SELECT Track.Id, Track.TrackNumber, Track.PreferedTramType, Track.SectorId, Track.TrackType FROM [Track] INNER JOIN dbo.Sector ON dbo.Track.Id = dbo.Sector.TrackId WHERE [Id] = @key", conn))
                {
                    cmd.Parameters.AddWithValue("@key", key);
                    conn.Open();
                    using(SqlDataReader datareader = cmd.ExecuteReader())
                    {
                        while(datareader.Read())
                        {
                            track.Id = datareader.GetInt32(0);
                            track.TrackNumber = datareader.GetInt32(1);
                            track.TramType = (TramType)datareader.GetInt32(2);
                            track.Sectors = new List<SectorDTO>();
                            track.Type = (TrackType)datareader.GetInt32(4);
                        }
                    }
                    conn.Close();
                }
            }
            return track;
        }

        public void Update(TrackDTO obj)
        {
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using(SqlCommand cmd= new SqlCommand($"UPDATE Track SET [PreferedTramType] = @TramType WHERE [Id] = @Id",conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@TramType", obj.TramType);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
