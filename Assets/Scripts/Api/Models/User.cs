using Newtonsoft.Json;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; private set; }

        [JsonProperty("password")]
        public string Password { get; private set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}