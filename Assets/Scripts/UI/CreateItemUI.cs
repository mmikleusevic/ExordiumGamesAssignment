using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;

public class CreateItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI retailerNameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI itemCategoryNameText;
    [SerializeField] private ImageLoader imageLoader;

    public void Instantiate(Item item)
    {
        itemNameText.text = item.name;
        priceText.text = item.price.ToString("#.##") + "HRK";
        StartCoroutine(imageLoader.LoadImageFromUrl(item.image_url));
        itemCategoryNameText.text = GameManager.Instance.GetItemCategory(item.item_category_id).name;
        retailerNameText.text = GameManager.Instance.GetRetailer(item.retailer_id).name;
    }
}
