using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.JsonSchemas {
    public class BlockDataSchema {
        [Newtonsoft.Json.JsonProperty("nom", Required = Newtonsoft.Json.Required.Always)]
        public string Nom { get; set; }

        [Newtonsoft.Json.JsonProperty("resultats", Required = Newtonsoft.Json.Required.Always)]
        public List<Resultat> Resultats { get; set; }

        [Newtonsoft.Json.JsonProperty("sigle", Required = Newtonsoft.Json.Required.Always)]
        public string Sigle { get; set; }

        [Newtonsoft.Json.JsonProperty("trimestre", Required = Newtonsoft.Json.Required.Always)]
        public int Trimestre { get; set; }
    }

    public class Resultat {
        [Newtonsoft.Json.JsonProperty("prenom", Required = Newtonsoft.Json.Required.Always)]
        public string Prenom { get; set; }

        [Newtonsoft.Json.JsonProperty("nom", Required = Newtonsoft.Json.Required.Always)]
        public string Nom { get; set; }

        [Newtonsoft.Json.JsonProperty("matricule", Required = Newtonsoft.Json.Required.Always)]
        public string Matricule { get; set; }

        [Newtonsoft.Json.JsonProperty("note", Required = Newtonsoft.Json.Required.Always)]
        public string Note { get; set; }
    }
}
