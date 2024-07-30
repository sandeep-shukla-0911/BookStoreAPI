using Newtonsoft.Json;

namespace BookStore.Models
{
    public class Users
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("username")]
        public string? UserName { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("role")]
        public string? Role { get; set; }

        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("fullName")]
        public string? FullName { get; set; }
    }
}
