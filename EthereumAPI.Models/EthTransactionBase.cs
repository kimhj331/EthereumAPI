using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAPI.Models
{
    public class EthTransactionBase
    {
        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        [JsonProperty("blockNumber")]
        public string BlockNumber { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("transactionIndex")]
        public string TransactionIndex { get; set; }
        
        [JsonProperty("hash")]
        public string Hash { set => TransactionHash = value; }

        [JsonProperty("transactionHash")]
        public string TransactionHash { get; set;  }

        [JsonProperty("type")]
        public string Type { get; set; }
    }


}
