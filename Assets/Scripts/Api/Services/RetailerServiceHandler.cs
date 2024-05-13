using ExordiumGamesAssignment.Scripts.Api.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class RetailerServiceHandler : ApiServiceHandler
    {
        public IEnumerator GetRetailers(Action<Retailer[]> callback)
        {
            string uri = baseUrl + "getretailers.php";

            using (UnityWebRequest request = UnityWebRequest.Get(uri))
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
                    Retailer[] retailers = JsonUtility.FromJson<Retailer[]>(jsonString);
                    callback?.Invoke(retailers);
                }
            }
        }
    }
}
