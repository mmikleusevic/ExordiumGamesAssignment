using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public class ImageLoader : MonoBehaviour
    {
        [SerializeField] private Image image;

        public IEnumerator LoadImageFromUrl(Action callback, string url)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(request.error);
                    callback?.Invoke();
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);

                    if (texture != null)
                    {
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                        image.sprite = sprite;
                        callback?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("Failed to create sprite: Texture is null.");
                        callback?.Invoke();
                    }
                }
            }
        }
    }
}
