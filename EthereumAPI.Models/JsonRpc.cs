using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAPI.Models
{
    public class JsonRpc<T>
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; } = "2.0";

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public List<T>? Params { get; set; }

        [JsonProperty("result")]
        public T? Result { get; set; }

        [JsonProperty("error")]
        public ErrorDetail? Error { get; set; }

    }
    public class ErrorDetail
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
