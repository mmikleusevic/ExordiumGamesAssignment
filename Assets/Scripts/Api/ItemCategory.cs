using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api
{
    public class ItemCategory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}