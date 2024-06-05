using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateItemUI : MonoBehaviour
    {
        public static event Action<Item, bool> OnFavoriteToggled;

        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI retailerNameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI itemCategoryNameText;
        [SerializeField] private ImageLoader imageLoader;
        [SerializeField] private Toggle favoriteToggle;

        private Item item;

        public void Instantiate(Item item)
        {
            StartCoroutine(imageLoader.LoadImageFromUrl(item.image_url, () => FilterItem()));
            this.item = item;
            itemNameText.text = item.name;
            priceText.text = item.price.ToString("C");
            itemCategoryNameText.text = GameManager.Instance.GetItemCategory(item.item_category_id).name;
            retailerNameText.text = GameManager.Instance.GetRetailer(item.retailer_id).name;

            favoriteToggle.onValueChanged.AddListener((value) =>
            {
                OnFavoriteToggled?.Invoke(this.item, value);
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
            favoriteToggle.onValueChanged.RemoveAllListeners();
        }

        private void FilterItem()
        {
            if (UserSettingsManager.Instance.GetFilterCategoryValue(item.item_category_id) && UserSettingsManager.Instance.GetFilterRetailerValue(item.retailer_id))
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public Item GetItem()
        {
            return item;
        }

        public bool GetToggleValue()
        {
            return favoriteToggle.isOn;
        }

        public void ToggleItem()
        {
            favoriteToggle.SetIsOnWithoutNotify(!favoriteToggle.isOn);
        }
    }
}
