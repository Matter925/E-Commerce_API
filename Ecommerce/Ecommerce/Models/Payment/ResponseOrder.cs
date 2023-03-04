using Newtonsoft.Json;

namespace Ecommerce.Models.Payment
{
    public class ResponseOrder
    {
        [JsonProperty("id")]
        public int id { get; set; }
    }
}
