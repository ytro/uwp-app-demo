using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.JsonSchemas {
    public class BlockchainResponse {
        [Newtonsoft.Json.JsonProperty("blocks", Required = Newtonsoft.Json.Required.Always)]
        public List<Block> Blocks { get; set; }
    }

    public class Block {
        [Newtonsoft.Json.JsonProperty("index", Required = Newtonsoft.Json.Required.Always)]
        public int Index { get; set; } // size_t

        [Newtonsoft.Json.JsonProperty("data", Required = Newtonsoft.Json.Required.Always)]
        public string Data { get; set; } // std::string

        [Newtonsoft.Json.JsonProperty("prevHash", Required = Newtonsoft.Json.Required.Always)]
        public string PreviousHash { get; set; } // std::string

        [Newtonsoft.Json.JsonProperty("timestamp", Required = Newtonsoft.Json.Required.Always)]
        public long Timestamp { get; set; } // time_t

        [Newtonsoft.Json.JsonProperty("hash", Required = Newtonsoft.Json.Required.Always)]
        public string Hash { get; set; } // std::string

        [Newtonsoft.Json.JsonProperty("nonce", Required = Newtonsoft.Json.Required.Always)]
        public int Nonce { get; set; } // size_t

        [Newtonsoft.Json.JsonProperty("validation", Required = Newtonsoft.Json.Required.Always)]
        public int Validation { get; set; } // size_t
    }
}