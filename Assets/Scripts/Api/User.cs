using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}