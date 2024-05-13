using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    public class AuthenticationResponse
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}