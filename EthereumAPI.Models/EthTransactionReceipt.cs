using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAPI.Models
{
    public class EthTransactionReceipt : EthTransactionBase
    {
        [JsonProperty("contractAddress")]
        public object ContractAddress { get; set; }
        [JsonProperty("cumulativeGasUsed")]
        public string CumulativeGasUsed { get; set; }
        [JsonProperty("effectiveGasPrice")]
        public string EffectiveGasPrice { get; set; }
        [JsonProperty("gasUsed")]
        public string GasUsed { get; set; }
        [JsonProperty("logs")]
        public List<Log>? Logs { get; set; }
        [JsonProperty("logsBloom")]
        public string LogsBloom { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
      
    }

    public class Log : EthTransactionBase
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("logIndex")]
        public string LogIndex { get; set; }
        [JsonProperty("removed")]
        public bool Removed { get; set; }
        [JsonProperty("topics")]
        public List<string> Topics { get; set; }
    }
}
