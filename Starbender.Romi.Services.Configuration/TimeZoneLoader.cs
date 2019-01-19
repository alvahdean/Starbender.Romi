using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Starbender.Romi.Services.Configuration
{
    using global::NodaTime;

    using Starbender.Contracts;
    using Starbender.Core.Data;
    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Core.Time;

    internal static class TimeZoneLoader
    {
        public static async Task Load(RomiDbContext dbContext, IExternalTimeZoneProvider provider)
        {
            await WriteZonesAsync(dbContext, provider);

            await WriteLinksAsync(dbContext, provider);

            await WriteIntervalsAsync(dbContext, provider);

            await WriteVersion(dbContext, provider);
        }

        private static async Task WriteZonesAsync(RomiDbContext dbContext,IExternalTimeZoneProvider provider)
        {
            var zones = provider.Ids;
            List<TimeZonePoco> result = new List<TimeZonePoco>();
            using (var uow = new UnitOfWork<TimeZonePoco>(dbContext))
            {
                foreach (var zone in zones)
                {
                    TimeZonePoco record = await uow.Repository().SingleOrDefaultAsync(t => t.Name == zone);
                    DateTimeOffset updateTime = DateTimeOffset.UtcNow;
                    if (record == null)
                    {

                        record = new TimeZonePoco() { Name = zone, Created = updateTime, Updated = updateTime };
                        uow.Repository().Add(record);
                        record = await uow.Repository().SingleOrDefaultAsync(t => t.Name == zone);
                    }
                    else
                    {
                        record.Updated = updateTime;
                        uow.Repository().Update(record);
                    }

                    result.Add(record);
                }
            }
        }

        private static async Task WriteLinksAsync(RomiDbContext dbContext, IExternalTimeZoneProvider provider)
        {
            foreach (var alias in provider.Aliases)
            {
                TimeZonePoco dbCanonical;

                using (var uowCanonical = new UnitOfWork<TimeZonePoco>(dbContext))
                {
                    dbCanonical = await uowCanonical.Repository().SingleOrDefaultAsync(t => t.Name == alias.Key);
                }

                foreach (var link in alias)
                {
                    TimeZonePoco dbLink;
                    using (var uowLink = new UnitOfWork<TimeZonePoco>(dbContext))
                    {
                        dbLink = await uowLink.Repository().SingleOrDefaultAsync(t => t.Name == link);
                    }

                    if (dbCanonical.Id == dbLink.Id)
                    {
                        continue;
                    }

                    using (var uow = new UnitOfWork<TimeZoneAliasPoco>(dbContext))
                    {
                        var repo = uow.Repository();
                        TimeZoneAliasPoco dbAlias = await repo.SingleOrDefaultAsync(
                                                        t => t.CanonicalId == dbCanonical.Id
                                                             && t.TimeZoneId == dbLink.Id);
                        if (dbAlias == null)
                        {
                            dbAlias = new TimeZoneAliasPoco() { CanonicalId = dbCanonical.Id, TimeZoneId = dbLink.Id };
                            repo.Add(dbAlias);
                        }
                    }
                }
            }
        }

        private static async Task WriteIntervalsAsync(RomiDbContext dbContext, IExternalTimeZoneProvider provider)
        {
            IDictionary<string, int> zones;

            var currentUtcYear = SystemClock.Instance.GetCurrentInstant().InUtc().Year;
            var maxYear = currentUtcYear + 5;
            var maxInstant = new LocalDate(maxYear + 1, 1, 1).AtMidnight().InUtc().ToInstant();

            var links = provider.Aliases.SelectMany(x => x).OrderBy(x => x).ToList();

            foreach (var id in provider.Ids)
            {
                // Skip noncanonical zones
                if (links.Contains(id))
                    continue;

                using (var dt = new DataTable())
                {
                    dt.Columns.Add("UtcStart", typeof(DateTime));
                    dt.Columns.Add("UtcEnd", typeof(DateTime));
                    dt.Columns.Add("LocalStart", typeof(DateTime));
                    dt.Columns.Add("LocalEnd", typeof(DateTime));
                    dt.Columns.Add("OffsetMinutes", typeof(short));
                    dt.Columns.Add("Abbreviation", typeof(string));

                    var intervals = provider[id].GetZoneIntervals(Instant.MinValue, maxInstant);
                    foreach (var interval in intervals)
                    {
                        TimeZoneIntervalPoco intervalPoco = new TimeZoneIntervalPoco
                        {
                            UtcStart = interval.Start == Instant.MinValue
                                                    ? DateTime.MinValue
                                                    : interval.Start.ToDateTimeUtc(),

                            UtcEnd = interval.End == Instant.MaxValue
                                                  ? DateTime.MaxValue
                                                  : interval.End.ToDateTimeUtc()
                        };

                        intervalPoco.LocalStart = intervalPoco.UtcStart == DateTime.MinValue
                                                      ? DateTime.MinValue
                                                      : interval.IsoLocalStart.ToDateTimeUnspecified();

                        intervalPoco.LocalEnd = intervalPoco.UtcEnd == DateTime.MaxValue
                                                    ? DateTime.MaxValue
                                                    : interval.IsoLocalEnd.ToDateTimeUnspecified();

                        intervalPoco.OffsetMinutes = (short)interval.WallOffset.ToTimeSpan().TotalMinutes;

                        var abbreviation = interval.Name;

                        if (abbreviation.StartsWith("Etc/"))
                        {
                            abbreviation = abbreviation.Substring(4);
                            if (abbreviation.StartsWith("GMT+"))
                                abbreviation = "GMT-" + abbreviation.Substring(4);
                            else if (abbreviation.StartsWith("GMT-"))
                                abbreviation = "GMT+" + abbreviation.Substring(4);
                        }

                        intervalPoco.Abbreviation = abbreviation;

                    }
                }
            }
        }

        private static async Task WriteVersion(RomiDbContext dbContext, IExternalTimeZoneProvider provider)
        {
            TimeZoneVersionPoco newRecord =
                new TimeZoneVersionPoco() { Version = provider.VersionId, Loaded = DateTimeOffset.UtcNow };
            using (var uow = new UnitOfWork<TimeZoneVersionPoco>(dbContext))
            {
                await Task.Run(() => uow.Repository().Add(newRecord));
            }
        }
    }
}