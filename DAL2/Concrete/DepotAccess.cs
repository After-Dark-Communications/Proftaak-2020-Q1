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
using Services;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Internal;

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
            depot.DepotTracks = new List<TrackDTO>();
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                //SELECT dbo.Depot.Id, dbo.Depot.Location, dbo.Track.TrackNumber, dbo.Sector.TramId, dbo.Tram.TramNumber FROM dbo.Depot RIGHT JOIN dbo.Track ON dbo.Depot.Id = dbo.Track.depotId RIGHT JOIN dbo.Sector ON dbo.Track.Id = dbo.Sector.TrackId RIGHT JOIN dbo.Tram ON dbo.Tram.Id = dbo.Sector.TramId WHERE [Id] = @key;
                using (SqlCommand cmd = new SqlCommand($"SELECT dbo.Depot.Id, dbo.Depot.Location, dbo.Track.Id, dbo.Track.TrackNumber, dbo.Track.PreferedLine, dbo.Track.PreferedTramType, dbo.Track.RepairServiceId, dbo.Sector.Id, dbo.Sector.Location, dbo.Sector.TramId, dbo.Tram.TramNumber, dbo.Tram.Type, dbo.Status_Tram.StatusId, dbo.Status_Tram.Description, dbo.Status_Tram.Id, dbo.Sector.SectorType FROM dbo.Depot RIGHT JOIN dbo.Track ON dbo.Depot.Id = dbo.Track.depotId RIGHT JOIN dbo.Sector ON dbo.Track.Id = dbo.Sector.TrackId LEFT JOIN dbo.Tram ON dbo.Sector.TramId = dbo.Tram.Id LEFT JOIN dbo.Status_Tram ON dbo.Tram.Id = dbo.Status_Tram.TramId WHERE [DepotId] = @key", conn))
                {
                    cmd.Parameters.AddWithValue("@key", key);
                    conn.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            TrackDTO track = new TrackDTO();
                            depot.Id = dataReader.GetInt32(0);
                            depot.Location = dataReader.GetString(1);
                            int trackId = dataReader.GetInt32(2);

                            if (!depot.DepotTracks.Any(i => i.Id == trackId))
                            {
                                track.Id = trackId;
                                track.TrackNumber = dataReader.GetInt32(3);
                                if (!dataReader.IsDBNull(5))
                                {
                                    track.TramType = (TramType)dataReader.GetInt32(5);
                                }
                                if(!dataReader.IsDBNull(4))
                                {
                                    track.PreferredTrackLine = dataReader.GetInt32(4);
                                }
                                depot.DepotTracks.Add(track);
                            }
                            int sectorId = dataReader.GetInt32(7);
                            if (!depot.DepotTracks.Any(track => track.Sectors.Any(sector => sector.Id == sectorId)))
                            {
                                SectorDTO sector = new SectorDTO();
                                sector.Id = sectorId;
                                sector.SectorPosition = dataReader.GetInt32(8);
                                var sectorType = (SectorType)dataReader.GetInt32(15);
                                sector.SectorType = sectorType;
                                sector.TrackNumber = dataReader.GetInt32(3);
                                depot.DepotTracks.FirstOrDefault(i => i.Id == dataReader.GetInt32(2)).Sectors.Add(sector);
                            }
                            if (!dataReader.IsDBNull(9))
                            {
                                int tramId = dataReader.GetInt32(9);
                                var foundTram = depot.DepotTracks.FirstOrDefault(i => i.Id == dataReader.GetInt32(2)).Sectors.FirstOrDefault(d => d.Id == dataReader.GetInt32(7)).Tram;
                                if (foundTram == null)
                                {
                                    TramDTO tram = new TramDTO
                                    {
                                        Id = tramId,
                                        TramNumber = dataReader.GetString(10),
                                        Type = (TramType)dataReader.GetInt32(11),
                                    };
                                    TrackDTO foundTrack = depot.DepotTracks.FirstOrDefault(i => i.Id == dataReader.GetInt32(2));
                                    foundTrack.Sectors.FirstOrDefault(i => i.Id == dataReader.GetInt32(7)).Tram = tram;
                                }
                                StatusDTO status = new StatusDTO();
                                if (!dataReader.IsDBNull(12))
                                {
                                    status.Status = (TramStatus)dataReader.GetInt32(12);
                                }
                                if (!dataReader.IsDBNull(13))
                                {
                                    status.Description = dataReader.GetString(13);
                                }
                                if (!dataReader.IsDBNull(14))
                                {
                                    status.StatusId = dataReader.GetInt32(14);
                                    TrackDTO foundTrack = depot.DepotTracks.FirstOrDefault(i => i.Id == dataReader.GetInt32(2));
                                    foundTrack.Sectors.FirstOrDefault(i => i.Id == dataReader.GetInt32(7)).Tram.Status.Add(status);
                                }
                            }                          
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
