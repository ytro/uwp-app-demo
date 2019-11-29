using PCApplication.JsonSchemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.Models {
    public class Blockchain {
        public static Blockchain Instance { get; private set; } = new Blockchain();

        public List<Block> Blocks { get; private set; }

        public Blockchain() { Blocks = new List<Block>(); }

        public bool Update(BlockchainResponse response) {
            Blocks.Clear();

            foreach (JsonSchemas.Block block in response.Blocks) {
                Blocks.Add(new Block(block.Index, block.Data, block.PreviousHash, block.Timestamp, block.Hash, block.Nonce, block.Validation));
            }

            return true;
        }

        public static bool Cleanup() {
            Instance.Blocks.Clear();
            return true;
        }
    }
}
