using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.JsonSchemas {
    public class UsersResponse {
        [Newtonsoft.Json.JsonProperty("comptes", Required = Newtonsoft.Json.Required.Always)]
        public List<User> Users { get; set; }
    }

    public class User {
        [Newtonsoft.Json.JsonProperty("usager", Required = Newtonsoft.Json.Required.Always)]
        public string Username { get; set; }

        [Newtonsoft.Json.JsonProperty("edition", Required = Newtonsoft.Json.Required.Always)]
        public bool IsEditor { get; set; }
    }
}
