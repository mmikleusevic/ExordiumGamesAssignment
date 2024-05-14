using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI retailerNameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI itemCategoryNameText;
        [SerializeField] private ImageLoader imageLoader;

        public int categoryId = -1;

        public void Instantiate(Item item)
        {
            StartCoroutine(imageLoader.LoadImageFromUrl(() => FilterItem(), item.image_url));
            categoryId = item.item_category_id;
            itemNameText.text = item.name;
            priceText.text = item.price.ToString("C");
            itemCategoryNameText.text = GameManager.Instance.GetItemCategory(item.item_category_id).name;
            retailerNameText.text = GameManager.Instance.GetRetailer(item.retailer_id).name;
        }

        private void FilterItem()
        {
            if (GameManager.Instance.GetFilterCategoryValue(categoryId))
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
