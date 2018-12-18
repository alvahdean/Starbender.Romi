using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Starbender.Romi.Data.Models;

namespace Starbender.Romi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SupportedInterface> SupportedInterfaces { get; set; }

        public DbSet<SupportedDevice> SupportedDevices { get; set; }

        public DbSet<DeviceRegistry> DeviceRegistry { get; set; }

        public DbSet<RomiSettings> RomiSettings { get; set; }
    }
}
