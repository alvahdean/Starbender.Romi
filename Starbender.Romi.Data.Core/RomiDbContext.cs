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

        public DbSet<RomiApplicationHost> ApplicationHosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=/apps/romi/data/romi.db", 
                builder => builder.MigrationsAssembly(typeof(RomiDbContext).Assembly.FullName));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<RomiApplicationHost>().HasData(new RomiApplicationHost { Id = 1, Name = "." });
        }
    }
}
