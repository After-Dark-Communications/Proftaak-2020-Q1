using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public class TramContext:DbContext
    {
        public TramContext(DbContextOptions<TramContext> options):base(options)
        {
                
        }
        public DbSet<Tram> Tram { get; set; }
        public DbSet<User> User { get; set; }
       
    }
}
