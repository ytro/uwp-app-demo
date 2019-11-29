using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.Models
{
    /// <summary>
    /// Represents a single log entry
    /// </summary>
    public class LogEntry
    {
        public int Number { get; private set; }
        public string Severity { get; private set; }
        public string DateAndTime { get; private set; }
        public HostEnum Source { get; private set; }
        public string Message { get; private set; }

        public LogEntry(int number, string severity, string dateAndTime, HostEnum source, string message)
        {
            Number = number;
            Severity = severity;
            DateAndTime = dateAndTime;
            Source = source;
            Message = message;
        }
    }
}
