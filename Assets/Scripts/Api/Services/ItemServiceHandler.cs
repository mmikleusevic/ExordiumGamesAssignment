using ExordiumGamesAssignment.Scripts.Api.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class ItemServiceHandler : ApiServiceHandler
    {
        [Serializable]
        private class ItemArrayWrapper
        {
            public Item[] root;
        }

        public IEnumerator GetItems(Action<Item[]> callback, int pageNumber = 1)
        {
            string uri = baseUrl + $"getitems.php?pageNumber={pageNumber}";

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

                    ItemArrayWrapper wrapper = JsonUtility.FromJson<ItemArrayWrapper>(wrappedJsonString);
                    Item[] items = wrapper.root;

                    callback?.Invoke(items);
                }
            }
        }
    }
}
