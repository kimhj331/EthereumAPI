using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EthereumAPI.Utilities
{
    public class OriginalPropertyContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            foreach (JsonProperty prop in list)
            {
                prop.PropertyName = System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(prop.UnderlyingName);
            }

            return list;
        }
    }
}
