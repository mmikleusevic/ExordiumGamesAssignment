using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateFavoriteUI : MonoBehaviour
    {
        public static event Action<Item> OnFavoriteRemoved;

        [SerializeField] protected TextMeshProUGUI itemNameText;
        [SerializeField] protected TextMeshProUGUI retailerNameText;
        [SerializeField] protected TextMeshProUGUI priceText;
        [SerializeField] protected TextMeshProUGUI itemCategoryNameText;
        [SerializeField] protected ImageLoader imageLoader;
        [SerializeField] private Button removeFavoriteButton;
        [SerializeField] private FavoritesUI favoritesUI;

        public int id = -1;

        public void Instantiate(Item item)
        {
            StartCoroutine(imageLoader.LoadImageFromUrl(item.image_url));
            id = item.id;
            itemNameText.text = item.name;
            priceText.text = item.price.ToString("C");
            itemCategoryNameText.text = GameManager.Instance.GetItemCategory(item.item_category_id).name;
            retailerNameText.text = GameManager.Instance.GetRetailer(item.retailer_id).name;

            removeFavoriteButton.onClick.AddListener(() =>
            {
                favoritesUI.RemoveFavoriteItem(item);

                OnFavoriteRemoved?.Invoke(item);

                Destroy(gameObject);
            });

            ThemeManager.Instance.AddImage(GetComponent<Image>());
            ThemeManager.Instance.AddOutline(GetComponent<Outline>());
            ThemeManager.Instance.AddTextElements(new List<TextMeshProUGUI>
            {
                itemNameText,
                retailerNameText,
                priceText,
                itemCategoryNameText,
            });
        }

        private void OnDestroy()
        {
            ThemeManager.Instance.RemoveImage(GetComponent<Image>());
            ThemeManager.Instance.Remove(GetComponent<Outline>());
            ThemeManager.Instance.RemoveTextElements(new List<TextMeshProUGUI>
            {
                itemNameText,
                retailerNameText,
                priceText,
                itemCategoryNameText,
            });
        }
    }
}
