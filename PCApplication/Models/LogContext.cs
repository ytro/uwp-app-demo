using PCApplication.JsonSchemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.Models
{
    /// <summary>
    /// The model for the logs. A collection of log entries and the related business logic (Add, update, cleanup log entries)
    /// </summary>
    public class LogContext
    {
        public static LogContext Instance { get; private set; } = new LogContext();

        public List<LogEntry> Logs = new List<LogEntry>();

        // Last received log number from server
        private Dictionary<HostEnum, int> lastReceivedLookup = new Dictionary<HostEnum, int>() {
            { HostEnum.Miner1, 0 },
            { HostEnum.Miner2, 0 },
            { HostEnum.Miner3, 0 },
            { HostEnum.WebServer, 0 }
        };

        public bool Update(LogsResponse rootObj, HostEnum source)
        {
            foreach (Information information in rootObj.Information)
            {
                Logs.Add(new LogEntry(information.No, information.Severite, information.Heure, source, information.Message));
            }
            lastReceivedLookup[source] = rootObj.Information[rootObj.Information.Count - 1].No;
            return true;
        }

        public int GetLastReceived(HostEnum source) {
            if (source != HostEnum.Undefined)
                return lastReceivedLookup[source];
            else return 0;
        }

        public string GetLogsText(HostEnum source) {
            StringBuilder sb = new StringBuilder();
            foreach (LogEntry entry in Logs) {
                if (source == HostEnum.Undefined)
                    sb.Append(GetLogText(entry));
                else if (source == entry.Source)
                    sb.Append(GetLogText(entry));
            }
            return sb.ToString();
        }

        public string GetLogText(LogEntry entry) {
            StringBuilder sb = new StringBuilder();
            string source = "";
            switch (entry.Source) {
                case HostEnum.Miner1: source = "Mineur 1"; break;
                case HostEnum.Miner2: source = "Mineur 2"; break;
                case HostEnum.Miner3: source = "Mineur 3"; break;
                case HostEnum.WebServer: source = "Serveur Web"; break;
            }
            sb.AppendLine(string.Format("{0}:{1}:{2}:{3}:{4}", entry.Number, entry.Severity, entry.DateAndTime, source, entry.Message));
            return sb.ToString();
        }

        public static bool Cleanup()
        {
            Instance.Logs.Clear();

            foreach (var key in Instance.lastReceivedLookup.Keys.ToList()) {
                Instance.lastReceivedLookup[key] = 0;
            }

            return true;
        }
    }
}
