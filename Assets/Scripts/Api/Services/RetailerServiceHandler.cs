using ExordiumGamesAssignment.Scripts.Api.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class RetailerServiceHandler : ApiServiceHandler
    {
        [Serializable]
        private class RetailerArrayWrapper
        {
            public Retailer[] root;
        }

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

                    string wrappedJsonString = "{\"root\":" + jsonString + "}";

                    RetailerArrayWrapper wrapper = JsonUtility.FromJson<RetailerArrayWrapper>(wrappedJsonString);
                    Retailer[] retailers = wrapper.root;

                    callback?.Invoke(retailers);
                }
            }
        }
    }
}
