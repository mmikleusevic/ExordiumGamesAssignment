using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    public class Retailer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }
    }
}