using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api
{
    public class ApiResponse
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}