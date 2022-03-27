using EthereumAPI.Models;

namespace EthereumAPI.Contracts
{
    public interface IEthereumClient
    {
        public Task<JsonRpc<string>> CreateNewBlockFilter();
        public Task<JsonRpc<List<string>>> GetNewBlocks(string filterId);
        public Task<JsonRpc<EthBlock>> GetBlcokByHash(string hash);
        public Task<JsonRpc<EthBlockDetail>> GetBlcokDetailByHash(string hash);
        public Task<JsonRpc<EthTransactionReceipt>> GetTransactionReciptByHash(string hash);
    }
}