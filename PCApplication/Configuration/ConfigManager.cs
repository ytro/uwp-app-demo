using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PCApplication.Configuration {
    /// <summary>
    /// This class manages the configuration file.
    /// The configuration file can contain anything, but should minimally contain:
    /// - The server IP address
    /// - The server port
    /// 
    /// </summary>
    public class ConfigManager {
        private const string configFilename = "config.ini";

        public static Dictionary<string, string> Config { get; private set; }


        public static bool LoadConfig() {
            Config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (!File.Exists(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, configFilename))) {
                Create(configFilename);
                return false;
            }
            foreach (string line in File.ReadLines(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, configFilename))) {
                string text = line.Trim();
                if (!text.StartsWith("#")) {
                    string[] array = text.Split(new char[] { '=' });
                    Config.Add(array[0], array[1]);
                }
            }
            return true;
        }

        private static void Create(string configFilename) {
            StringBuilder sr = new StringBuilder();
            sr.AppendLine("ServerIP=127.0.0.1");
            sr.AppendLine("Port=8080");
            File.WriteAllText(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, configFilename), sr.ToString());
        }

        public static bool Contains(string key) {
            return Config.ContainsKey(key);
        }

        public static string GetValueFromKey(string key, string defaultValue = "") {
            if (!Config.ContainsKey(key)) {
                return defaultValue;
            }
            return Config[key];
        }


        public static string GetBaseServerUri() {
            return $"https://{GetValueFromKey("ServerIP")}:{GetValueFromKey("port")}";
        }
    }
}
