using System;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    [Serializable]
    public class AuthenticationResponse
    {
        public bool isSuccessful;
        public string message;
    }
}