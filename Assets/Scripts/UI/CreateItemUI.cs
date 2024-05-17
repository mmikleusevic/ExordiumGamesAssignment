using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateItemUI : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI itemNameText;
        [SerializeField] protected TextMeshProUGUI retailerNameText;
        [SerializeField] protected TextMeshProUGUI priceText;
        [SerializeField] protected TextMeshProUGUI itemCategoryNameText;
        [SerializeField] protected ImageLoader imageLoader;
        [SerializeField] private Toggle favoriteToggle;

        public int categoryId = -1;
        public int retailerId = -1;

        public virtual void Instantiate(Item item)
        {
            StartCoroutine(imageLoader.LoadImageFromUrl(item.image_url, () => FilterItem()));
            categoryId = item.item_category_id;
            retailerId = item.retailer_id;
            itemNameText.text = item.name;
            priceText.text = item.price.ToString("C");
            itemCategoryNameText.text = GameManager.Instance.GetItemCategory(item.item_category_id).name;
            retailerNameText.text = GameManager.Instance.GetRetailer(item.retailer_id).name;
            favoriteToggle.onValueChanged.AddListener((value) =>
            {

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
            if (UserSettingsManager.Instance.GetFilterCategoryValue(categoryId) && UserSettingsManager.Instance.GetFilterRetailerValue(retailerId))
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
