using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("retailer_id")]
        public int RetailerId { get; set; }

        [JsonProperty("item_category_id")]
        public int ItemCategoryId { get; set; }
    }
}