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

        public int Update(LogsSchema.RootObject rootObj, HostEnum source)
        {
            foreach (LogsSchema.Information information in rootObj.Information)
            {
                Logs.Add(new LogEntry(information.No, information.Severite, information.Heure, source, information.Message));
            }
            return Logs.Count;
        }

        public string GetLogsText() {
            StringBuilder sb = new StringBuilder();
            foreach (LogEntry entry in Logs) {
                string s = "";
                switch (entry.Source) {
                    case HostEnum.Miner1: s = "Mineur 1"; break;
                    case HostEnum.Miner2: s = "Mineur 2"; break;
                    case HostEnum.Miner3: s = "Mineur 3"; break;
                    case HostEnum.WebServer: s = "Serveur Web"; break;
                }
                sb.AppendLine(string.Format("{0}:{1}:{2}:{3}:{4}", entry.Number, entry.Severity, entry.DateAndTime, s, entry.Message));
            }
            return sb.ToString();
        }

        public string GetLogsText(HostEnum source) {
            StringBuilder sb = new StringBuilder();
            foreach (LogEntry entry in Logs) {
                if (entry.Source == source) {
                    string s = "";
                    switch (source) {
                        case HostEnum.Miner1: s = "Mineur 1"; break;
                        case HostEnum.Miner2: s = "Mineur 2"; break;
                        case HostEnum.Miner3: s = "Mineur 3"; break;
                        case HostEnum.WebServer: s = "Serveur Web"; break;
                    }
                    sb.AppendLine(string.Format("{0}:{1}:{2}:{3}:{4}", entry.Number, entry.Severity, entry.DateAndTime, s, entry.Message));
                }
            }
            return sb.ToString();
        }
        public static bool Cleanup()
        {
            Instance.Logs.RemoveAll(e => true);
            return true;
        }
    }
}
