using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public class DepotContext:DbContext
    {
        public DepotContext(DbContextOptions<DepotContext> options):base(options)
        {
                
        }
        public DbSet<Tram> Tram { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Depot> Depot { get; set; }
        public DbSet<Status> Status { get; set; }
       
    }
}
