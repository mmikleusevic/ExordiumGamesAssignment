using Newtonsoft.Json;
using System;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    [Serializable]
    public class ItemCategory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}