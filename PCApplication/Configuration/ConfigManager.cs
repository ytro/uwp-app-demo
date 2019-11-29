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
        private const string DefaultServerIP = "127.0.0.1";
        private const string DefaultServerPort = "443";
        private const string configFilename = "config.ini";

        public static Dictionary<string, string> Config { get; private set; }

        public static bool LoadConfig() {
            Config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // If config file doesn't exist locally, create a new one
            if (!File.Exists(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, configFilename))) {
                Create(configFilename);
            } else { // Else read from file
                foreach (string line in File.ReadLines(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, configFilename))) {
                    string text = line.Trim();
                    if (!text.StartsWith("#")) {
                        string[] array = text.Split(new char[] { '=' });
                        Config.Add(array[0], array[1]);
                    }
                }
            }
            if (!Config.ContainsKey("ServerIP"))
                Config.Add("ServerIP", DefaultServerIP);

            if (!Config.ContainsKey("Port"))
                Config.Add("Port", DefaultServerPort);

            return true;
        }

        public static string GetServerIP() {
            return GetValueFromKey("ServerIP");
        }

        public static string GetPort() {
            return GetValueFromKey("Port");

        }

        private static void Create(string configFilename) {
            StringBuilder sr = new StringBuilder();
            sr.AppendLine($"ServerIP={DefaultServerIP}");
            sr.AppendLine($"Port={DefaultServerPort}");
            File.WriteAllText(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, configFilename), sr.ToString());
        }

        public static void Update(string serverIP, string port) {
            Config["ServerIP"] = serverIP;
            Config["Port"] = port;

            StringBuilder sr = new StringBuilder();
            sr.AppendLine($"ServerIP={serverIP}");
            sr.AppendLine($"Port={port}");
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
            string Port = GetValueFromKey("Port");
            if (Port == "")
                return $"https://{GetValueFromKey("ServerIP")}";
            else
                return $"https://{GetValueFromKey("ServerIP")}:{GetValueFromKey("Port")}";
        }
    }
}
