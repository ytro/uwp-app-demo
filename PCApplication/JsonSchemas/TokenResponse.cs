using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.JsonSchemas {
    public class TokenResponse {
        [Newtonsoft.Json.JsonProperty("accessToken", Required = Newtonsoft.Json.Required.Always)]
        public string AccessToken { get; set; }

        [Newtonsoft.Json.JsonProperty("edition", Required = Newtonsoft.Json.Required.AllowNull)]
        public bool Edition { get; set; }
    }
}
