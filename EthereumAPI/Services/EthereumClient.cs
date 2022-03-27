using EthereumAPI.Contracts;
using EthereumAPI.Logger;
using EthereumAPI.Models;
using Newtonsoft.Json;

namespace EthereumAPI.Services
{
    public class EthereumClient : IEthereumClient
    {
        private ILoggerManager _logger;
        private HttpClient _client;
        private readonly string _infraUrl = "https://mainnet.infura.io/v3/f70d660ebc224abbb67b20da4a10ef7c";

        public EthereumClient(ILoggerManager logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client.CreateClient();
        }

        public async Task<JsonRpc<string>> CreateNewBlockFilter()
        {
            JsonRpc<object> requset = new JsonRpc<object>();
            requset.Method = InfraMethods.eth_NewBlockFilter;
            requset.Params = null;
            requset.Id = 1;

            var reponse = await SendPost(requset); 

            return JsonConvert.DeserializeObject<JsonRpc<string>>(reponse);
        }

        public async Task<JsonRpc<List<string>>> GetNewBlocks(string filterId)
        {
            JsonRpc<string> requset = new JsonRpc<string>();
            requset.Method = InfraMethods.eth_GetFilterChanges;
            requset.Params = new List<string> { filterId };
            requset.Id = 1;

            var response = await SendPost(requset);

            return JsonConvert.DeserializeObject<JsonRpc<List<string>>> (response);
        }

        public async Task<JsonRpc<EthBlock>> GetBlcokByHash(string hash)
        {
            JsonRpc<object> requset = new JsonRpc<object>();
            requset.Method = InfraMethods.eth_GetBlockByHash;
            requset.Params = new List<object> { hash, false };
            requset.Id = 1;

            var response = await SendPost(requset);

            return JsonConvert.DeserializeObject<JsonRpc<EthBlock>>(response);
        }

        public async Task<JsonRpc<EthBlockDetail>> GetBlcokDetailByHash(string hash)
        {
            JsonRpc<object> requset = new JsonRpc<object>();
            requset.Method = InfraMethods.eth_GetBlockByHash;
            requset.Params = new List<object> { hash, true };
            requset.Id = 1;

            var response = await SendPost(requset);
            return JsonConvert.DeserializeObject<JsonRpc<EthBlockDetail>>(response);
        }

        public async Task<JsonRpc<EthTransactionReceipt>> GetTransactionReciptByHash(string hash)
        {
            JsonRpc<string> requset = new JsonRpc<string>();
            requset.Method = InfraMethods.eth_GetTransactionReceipt;
            requset.Params = new List<string> { hash };
            requset.Id = 1;

            var response = await SendPost(requset);
            return JsonConvert.DeserializeObject<JsonRpc<EthTransactionReceipt>>(response);
        }

        private async Task<string> SendPost(object requestContent)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(requestContent));
            var response = await _client.PostAsync(_infraUrl, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
