using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api
{
    public class ItemsResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}