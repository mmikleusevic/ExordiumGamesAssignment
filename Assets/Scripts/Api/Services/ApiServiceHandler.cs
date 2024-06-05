using System;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class ApiServiceHandler
    {
        protected readonly string baseUrl;

        public ApiServiceHandler()
        {
            baseUrl = Environment.GetEnvironmentVariable("BASE_URL", EnvironmentVariableTarget.Process);
        }
    }
}
