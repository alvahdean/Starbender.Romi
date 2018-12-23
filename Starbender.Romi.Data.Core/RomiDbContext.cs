using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Starbender.Romi.Data.Models;

namespace Starbender.Romi.Data
{
    public class RomiDbContext : IdentityDbContext<RomiUser,RomiRole,string>
    {
        public RomiDbContext(DbContextOptions<RomiDbContext> options)
            : base(options)
        {
        }

        public DbSet<RegisteredInterface> Interfaces { get; set; }

        public DbSet<RegisteredDevice> Devices { get; set; }

        public DbSet<RomiSettings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
