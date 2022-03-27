using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAPI.Models
{
    public static class InfraMethods
    {
        public const string eth_NewBlockFilter = "eth_newBlockFilter";
        public const string eth_GetFilterChanges = "eth_getFilterChanges";
        public const string eth_GetBlockByHash = "eth_getBlockByHash";
        public const string eth_GetTransactionReceipt = "eth_getTransactionReceipt";
    }
}
