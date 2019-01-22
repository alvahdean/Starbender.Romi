using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models
{
    public class SystemInfoModel
    {
        /// <summary>Gets the wiring pi library version.</summary>
        public Version WiringPiVersion { get; }

        /// <summary>Gets the OS information.</summary>
        /// <value>The os information.</value>
        public OsInfoModel OperatingSystem { get; }

        /// <summary>Gets the Raspberry Pi version.</summary>
        public string RaspberryPiVersion { get; }

        /// <summary>Gets the Wiring Pi board revision (1 or 2).</summary>
        /// <value>The wiring pi board revision.</value>
        public int WiringPiBoardRevision { get; }

        /// <summary>Gets the number of processor cores.</summary>
        public int ProcessorCount { get; }

        /// <summary>Gets the installed ram in bytes.</summary>
        public int InstalledRam { get; }

        /// <summary>
        /// Gets a value indicating whether this CPU is little endian.
        /// </summary>
        public bool IsLittleEndian { get; }

        /// <summary>Gets the CPU model name.</summary>
        public string ModelName { get; private set; }

        /// <summary>Gets a list of supported CPU features.</summary>
        public string[] Features { get; private set; }

        /// <summary>Gets the CPU implementer hex code.</summary>
        public string CpuImplementer { get; private set; }

        /// <summary>Gets the CPU architecture code.</summary>
        public string CpuArchitecture { get; private set; }

        /// <summary>Gets the CPU variant code.</summary>
        public string CpuVariant { get; private set; }

        /// <summary>Gets the CPU part code.</summary>
        public string CpuPart { get; private set; }

        /// <summary>Gets the CPU revision code.</summary>
        public string CpuRevision { get; private set; }

        /// <summary>Gets the hardware model number.</summary>
        public string Hardware { get; private set; }

        /// <summary>Gets the hardware revision number.</summary>
        public string Revision { get; private set; }

        /// <summary>Gets the serial number.</summary>
        public string Serial { get; private set; }

        /// <summary>Gets the system up-time (in seconds).</summary>
        public double Uptime { get; }

        /// <summary>Gets the uptime in TimeSpan.</summary>
        public TimeSpan UptimeTimeSpan { get; }
    }
}
