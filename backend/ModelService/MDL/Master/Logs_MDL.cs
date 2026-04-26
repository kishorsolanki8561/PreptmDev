using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class Logs_MDL
    {
        public long LogId { get; set; }
        public long LogLevel { get; set; }
        public string? NewLine { get; set; }
        public string? SourceContext { get; set; }
        public string? CorrelationId { get; set; }
        public string? ProcessName { get; set; }
        public long ProcessId { get; set; }
        public long ThreadId { get; set; }
        public string? MachineName { get; set; }
        public string? ClientIp { get; set; }
        public string? EnvironmentName { get; set; }
    }

}
