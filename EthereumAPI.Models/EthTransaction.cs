using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAPI.Models
{
   
    public class EthTransaction : EthTransactionBase
    {
        [JsonProperty("accessList")]
        public List<object>? AccessList { get; set; }

        [JsonProperty("chainId")]
        public string ChainId { get; set; }

        [JsonProperty("gas")]
        public string Gas { get; set; }
        [JsonProperty("gasPrice")]
        public string GasPrice { get; set; }

        [JsonProperty("input")]
        public string Input { get; set; }
        [JsonProperty("maxFeePerGas")]
        public string MaxFeePerGas { get; set; }
        [JsonProperty("maxPriorityFeePerGas")]
        public string MaxPriorityFeePerGas { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        public string r { get; set; }
        public string s { get; set; }

        public string v { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public TransactionState TransactionState
        {
            get
            {
                if (Input != "0x")
                    return string.IsNullOrEmpty(this.To) ? TransactionState.TokenCreated : TransactionState.TokenTransfer;
                return TransactionState.Transfer;
            }
        }
    }

    public enum TransactionState : byte
    { 
        Transfer,
        TokenTransfer,
        TokenCreated,
    }
}

