using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models
{
    public class ProcessResultModel
    {
        /// <summary>Gets the exit code.</summary>
        /// <value>The exit code.</value>
        public int ExitCode { get; }
        /// <summary>Gets the text of the standard output.</summary>
        /// <value>The standard output.</value>
        public string StandardOutput { get; }
        /// <summary>Gets the text of the standard error.</summary>
        /// <value>The standard error.</value>
        public string StandardError { get; }
    }
}
