using System;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    [Serializable]
    public class AuthenticationResponse
    {
        public static readonly string WELCOME = "Welcome";
        public static readonly string EMAIL_TAKEN = "Email taken.";
        public static readonly string ACCOUNT_CREATED = "Account created.";
        public static readonly string INVALID_EMAIL_OR_PASSWORD = "Invalid email or password.";

        public bool isSuccessful;
        public string message;
    }

    public enum AuthResponse
    {
        REGISTER_SUCCESS,
        REGISTER_FAILURE,
        LOGIN_SUCCESS,
        LOGIN_FAILURE
    }
}