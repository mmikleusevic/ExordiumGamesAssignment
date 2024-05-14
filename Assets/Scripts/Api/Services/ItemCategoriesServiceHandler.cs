using ExordiumGamesAssignment.Scripts.Api.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class ItemCategoriesServiceHandler : ApiServiceHandler
    {
        [Serializable]
        private class ItemCategoryArrayWrapper
        {
            public ItemCategory[] root;
        }

        public IEnumerator GetItemCategories(Action<ItemCategory[]> callback)
        {
            string uri = baseUrl + "getitemcategories.php";

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

                    ItemCategoryArrayWrapper wrapper = JsonUtility.FromJson<ItemCategoryArrayWrapper>(wrappedJsonString);
                    ItemCategory[] itemCategories = wrapper.root;

                    callback?.Invoke(itemCategories);
                }
            }
        }
    }
}
