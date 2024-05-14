using Newtonsoft.Json;
using System;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    [Serializable]
    public class AuthenticationResponse
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}