namespace Starbender.Romi.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Starbender.Romi.Data.Models;

    public class RomiDbContext : IdentityDbContext<RomiUser, RomiRole, string>
    {
        public RomiDbContext()
            : base()
        {

        }

        public RomiDbContext(DbContextOptions<RomiDbContext> options)
            : base(options)
        {
        }

        public DbSet<RomiApplicationHost> ApplicationHosts { get; set; }

        public DbSet<DeviceProperty> DeviceProperties { get; set; }

        public DbSet<RegisteredDevice> Devices { get; set; }

        public DbSet<InterfaceProperty> InterfaceProperties { get; set; }

        public DbSet<RegisteredInterface> Interfaces { get; set; }

        public DbSet<SensorResult> SensorResults { get; set; }

        public DbSet<SensorPoco> Sensors { get; set; }

        public DbSet<RomiSettings> Settings { get; set; }

        public DbSet<TimeZoneIntervalPoco> TimeZoneIntervals { get; set; }

        public DbSet<TimeZonePoco> TimeZones { get; set; }

        public DbSet<TimeZoneVersionPoco> TimeZoneVersions { get; set; }

        //public DbSet<TimeZoneAliasPoco> TimeZoneAliases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=/apps/romi/data/romi.db",
                builder => builder.MigrationsAssembly(typeof(RomiDbContext).Assembly.FullName));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}