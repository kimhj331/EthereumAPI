using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAPI.Models
{
    public class EthBlock
    {
        [JsonProperty("baseFeePerGas")]
        public string BaseFeePerGas { get; set; }
        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }
        [JsonProperty("extraData")]
        public string ExtraData { get; set; }
        [JsonProperty("GasLimit")]
        public string gasLimit { get; set; }
        [JsonProperty("gasUsed")]
        public string GasUsed { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
        [JsonProperty("logsBloom")]
        public string LogsBloom { get; set; }
        [JsonProperty("miner")]
        public string Miner { get; set; }
        [JsonProperty("mixHash")]
        public string MixHash { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("parentHash")]
        public string ParentHash { get; set; }
        [JsonProperty("receiptsRoot")]
        public string ReceiptsRoot { get; set; }
        [JsonProperty("sha3Uncles")]
        public string Sha3Uncles { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
        [JsonProperty("stateRoot")]
        public string StateRoot { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("totalDifficulty")]
        public string TotalDifficulty { get; set; }

        [JsonProperty("transactions")]
        public List<string> Transactions { get; set; }

        [JsonProperty("transactionsRoot")]
        public string TransactionsRoot { get; set; }
        [JsonProperty("uncles")]
        public List<object> Uncles { get; set; }
    }

    public class EthBlockDetail : EthBlock
    {
        [JsonProperty("transactions")]
        public new List<EthTransaction> Transactions { get; set; }
    }
   
}
