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

        public async Task Create(SectorDTO obj)
        {
            using(SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                throw new NotImplementedException();
            }
        }

        public async Task Delete(int key)
        {
            throw new NotImplementedException();
        }

        public SectorDTO Read(int key)
        {
            SectorDTO sector = new SectorDTO();
            //using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            //{
            //    conn.Open();
            //    using(SqlCommand cmd = new SqlCommand($" SELECT dbo.Track.Id, dbo.Sector.TramId, dbo.Sector.Location, dbo.Track.TrackNumber FROM dbo.Sector INNER JOIN dbo.Track ON dbo.Sector.Id = dbo.Track.Id INNER JOIN dbo.Tram ON dbo.Sector.Id = dbo.Tram.Id WHERE[dbo.Sector.Id] = @key"))
            //    {
            //        using (SqlDataReader dataReader = cmd.ExecuteReader())
            //        {
            //            while(dataReader.Read())
            //            {
            //                sector.Id = dataReader.GetInt32(key);
            //                sector.TramId = dataReader.GetInt32(1);
            //                sector.SectorPosition = dataReader.GetInt32(2);
            //                sector.TrackNumber = dataReader.GetInt32(3);
            //            }
            //        }
            //    }
            //    conn.Close();
            //}
            return sector;
        }

        public async Task Update(SectorDTO obj)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection._connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"UPDATE [dbo.Sector] WHERE [Id] = @obj.Id"))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                conn.Close();
            }
        }
    }
}
