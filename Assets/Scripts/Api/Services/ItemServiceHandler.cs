using ExordiumGamesAssignment.Scripts.Api.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class ItemServiceHandler : ApiServiceHandler
    {
        private int pageNumber = 1;

        [Serializable]
        private class ItemArrayWrapper
        {
            public Item[] root;
        }

        public IEnumerator GetPagedItems(Action<Item[]> callback)
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

                    if (!jsonString.Contains("{") && !jsonString.Contains("}"))
                    {
                        callback?.Invoke(null);
                        yield break;
                    }

                    string wrappedJsonString = "{\"root\":" + jsonString + "}";

                    ItemArrayWrapper wrapper = JsonUtility.FromJson<ItemArrayWrapper>(wrappedJsonString);
                    pageNumber++;

                    callback?.Invoke(wrapper.root);
                }
            }
        }

        public IEnumerator GetAllItems(Action<Item[]> callback)
        {
            string uri = baseUrl + $"getitems.php";

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

                    callback?.Invoke(wrapper.root);
                }
            }
        }
    }
}
