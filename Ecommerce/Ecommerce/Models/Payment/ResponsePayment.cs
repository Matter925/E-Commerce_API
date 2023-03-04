using Newtonsoft.Json;

namespace Ecommerce.Models.Payment
{
    public class ResponsePayment
    {
        [JsonProperty("obj")]
        public ResponseObj obj { get; set; }


    }
}
