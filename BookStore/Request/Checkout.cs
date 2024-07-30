using Newtonsoft.Json;

namespace BookStore.Request
{
    public class Checkout
    {
        [JsonProperty("bookIds")]
        public int[] BookIds { get; set; }

        public double? Price { get; set; }
    }
}
