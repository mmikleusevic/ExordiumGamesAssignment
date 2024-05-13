using ExordiumGamesAssignment.Scripts.Api.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class UserServiceHandler : ApiServiceHandler
    {
        public IEnumerator LoginOrRegister(Action<AuthenticationResponse> callback, User user, bool register)
        {
            string uri = baseUrl;

            if (register)
            {
                uri += "register.php";
            }
            else
            {
                uri += "login.php";
            }

            WWWForm form = new WWWForm();

            form.AddField("username", user.Username);
            form.AddField("password", user.Password);

            using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    callback?.Invoke(null);
                }
                else
                {
                    string jsonString = request.downloadHandler.text;
                    AuthenticationResponse apiResponse = JsonUtility.FromJson<AuthenticationResponse>(jsonString);
                    callback?.Invoke(apiResponse);
                }
            }
        }
    }
}
