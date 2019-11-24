using Newtonsoft.Json;
using NJsonSchema;
using PCApplication.JsonSchemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.Models
{
    public class Block
    {
        public int Index { get; set; } // size_t
        public BlockData Data { get; set; } // std::string
        public string PreviousHash { get; set; } // std::string
        public DateTime Timestamp { get; set; } // time_t
        public string Hash { get; set; } // std::string
        public int Nonce { get; set; } // size_t

        public Block(int index, string data, string previousHash, long timestamp, string hash, int nonce) {
            Index = index;
            Data = new BlockData(data);
            PreviousHash = previousHash;
            Timestamp = new DateTime(1970, 1, 1).AddSeconds(timestamp);
            Hash = hash;
            Nonce = nonce;
        }

        public class BlockData 
        {
            public string Nom { get; set; } = "Non disponible";
            public List<Resultat> Resultats { get; set; } = new List<Resultat>();
            public string Sigle { get; set; } = "Non disponible";
            public string Trimestre { get; set; } = "Non disponible";

            private string _summary = "";
            public string Summary {
                get {
                    if (_summary != "")
                        return _summary;
                    else
                        return $"{Sigle} - {Trimestre}";
                }
                set {
                    _summary = value;
                }
            }

            public BlockData(string data) {
                JsonSchema schema = JsonSchema.FromType<BlockDataSchema>();
                try {
                    var errors = schema.Validate(data);
                    if (errors.Count == 0) {
                        BlockDataSchema deserializedData = JsonConvert.DeserializeObject<BlockDataSchema>(data);

                        Nom = deserializedData.Nom;
                        foreach (JsonSchemas.Resultat resultat in deserializedData.Resultats) {
                            Resultats.Add(new Resultat(resultat.Prenom, resultat.Nom, resultat.Matricule, resultat.Note));
                        }
                        Sigle = deserializedData.Sigle;
                        Trimestre = deserializedData.Trimestre.ToString();
                    }
                } catch {
                    if (data.Contains("GenesysBlock")) {
                        Summary = "GenesysBlock";
                    }
                }
            }

            public class Resultat {
                public string Prenom { get; set; }
                public string Nom { get; set; }
                public string Matricule { get; set; }
                public string Note { get; set; }

                public Resultat(string prenom, string nom, string matricule, string note) {
                    Prenom = prenom;
                    Nom = nom;
                    Matricule = matricule;
                    Note = note;
                }
            }
        }
    }


}
