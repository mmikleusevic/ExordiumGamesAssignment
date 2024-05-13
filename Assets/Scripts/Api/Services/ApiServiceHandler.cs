using System.Net.Http;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class ApiServiceHandler
    {
        protected static readonly HttpClient httpClient = new HttpClient();
        protected readonly string baseUrl = "https://exordiumgames.com/unity_backend_assignment/";
    }
}
