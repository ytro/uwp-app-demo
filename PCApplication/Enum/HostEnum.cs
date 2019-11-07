using System.ComponentModel;

namespace PCApplication {
    public enum HostEnum {
        [Description("Mineur 1")]
        Miner1 = 1,

        [Description("Mineur 2")]
        Miner2 = 2,

        [Description("Mineur 3")]
        Miner3 = 3,

        [Description("Serveur Web")]
        WebServer = 4
    }
}
