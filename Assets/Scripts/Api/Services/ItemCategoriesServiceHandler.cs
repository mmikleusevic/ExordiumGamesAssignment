using ExordiumGamesAssignment.Scripts.Api.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ExordiumGamesAssignment.Scripts.Api.Services
{
    public class ItemCategoriesServiceHandler : ApiServiceHandler
    {
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
                    ItemCategory[] itemCategories = JsonUtility.FromJson<ItemCategory[]>(jsonString);
                    callback?.Invoke(itemCategories);
                }
            }
        }
    }
}
