using System;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class ApiServiceHandler
    {
        protected readonly string baseUrl;

        public ApiServiceHandler()
        {
            baseUrl = Environment.GetEnvironmentVariable("BASE_URL");
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("BASE_URL environment variable is not set.");
            }
        }
    }
}
