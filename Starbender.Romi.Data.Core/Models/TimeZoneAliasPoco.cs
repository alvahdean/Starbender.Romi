namespace Starbender.Romi.Data.Models
{
    public class TimeZoneAliasPoco
    {
        public int? CanonicalId { get; set; }

        public TimeZonePoco CanonicalZone { get; set; }

        public TimeZonePoco TimeZone { get; set; }

        public int TimeZoneId { get; set; }
    }
}