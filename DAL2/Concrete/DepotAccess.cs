using AutoMapper;
using Context;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using DAL.Context;

namespace DAL.Concrete
{
    public class DepotAccess : IDepotAccess
    {
        private readonly DepotContext _context;
        private readonly IMapper _mapper;
        public DepotAccess(DepotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Create(DepotDTO obj)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO [dbo.Depot]([Location]) VALUES (@Location)", conn))
                {
                    command.Parameters.AddWithValue("@Location", obj.Location);
                    command.ExecuteNonQueryAsync();
                    conn.Close();
                }
            }
        }
        public DepotDTO Read(int key)
        {
            DepotDTO depot = new DepotDTO();
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Depot] WHERE [Id] = @key", conn))
                {
                    cmd.Parameters.AddWithValue("@key", key);
                    conn.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            depot.Id = dataReader.GetInt32(0);
                            depot.Location = dataReader.GetString(1);
                        }
                    }
                }
                conn.Close();
            }
            return depot;
        }

        public void Update(DepotDTO obj)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"UPDATE Depot SET [location] = @Location WHERE [Id] = @Id ", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Location", obj.Location);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Delete(int key)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [dbo.Depot] WHERE [Id] = @key"))
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public IEnumerable<TrackDTO> GetAllTracks(DepotDTO depot)
        {
            List<TrackDTO> tracks = new List<TrackDTO>();
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.Track.TrackNumber FROM dbo.Depot INNER JOIN dbo.Track ON dbo.Depot.Id = dbo.Track.DepotId;"))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int id = dataReader.GetInt32(0);
                            int trackNumber = dataReader.GetInt32(1);
                            TrackDTO track = new TrackDTO
                            {
                                Id = id,
                                TrackNumber = trackNumber
                            };
                            tracks.Add(track);
                        }
                    }
                }
            }
            return tracks;
        }
    }
}
