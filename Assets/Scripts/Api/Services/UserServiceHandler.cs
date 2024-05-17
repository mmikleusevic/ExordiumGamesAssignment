using ExordiumGamesAssignment.Scripts.Api.Models;
using Newtonsoft.Json;
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

            form.AddField("json", JsonConvert.SerializeObject(user));

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
                    AuthenticationResponse authenticationResponse = JsonUtility.FromJson<AuthenticationResponse>(jsonString);
                    callback?.Invoke(authenticationResponse);
                }
            }
        }
    }
}