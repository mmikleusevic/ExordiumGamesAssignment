using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateFavoriteUI : CreateItemUI
    {
        [SerializeField] private Button removeFavoriteButton;

        public override void Instantiate(Item item)
        {
            base.Instantiate(item);

            removeFavoriteButton.onClick.AddListener(() =>
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

                UserSettingsManager.Instance.RemoveFavorite(item.id);

                Destroy(gameObject);
            });
        }
    }
}
