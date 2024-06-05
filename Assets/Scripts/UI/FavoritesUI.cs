using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class FavoritesUI : MonoBehaviour
    {
        private readonly string FAVORITES = "favorites";

        [SerializeField] protected TextMeshProUGUI headerText;
        [SerializeField] protected Transform container;
        [SerializeField] protected Transform template;
        [SerializeField] protected ScrollRect scrollRect;

        private List<Item> favoriteItems;

        private void Awake()
        {
            favoriteItems = new List<Item>();

            template.gameObject.SetActive(false);

            gameObject.SetActive(false);

            UserSettingsManager.Instance.OnFiltersChanged += UserSettingsManager_OnFiltersChanged;
            CreateItemUI.OnFavoriteToggled += CreateItemUI_OnFavoriteToggled;
        }

        private void OnEnable()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, FAVORITES);
        }

        private void OnDisable()
        {
            UserSettingsManager.Instance?.SaveSelectedUserFavoritesSettings();

            foreach (Transform child in container)
            {
                if (template == child) continue;

                Destroy(child.gameObject);
            }
        }

        private void OnDestroy()
        {
            CreateItemUI.OnFavoriteToggled -= CreateItemUI_OnFavoriteToggled;
            UserSettingsManager.Instance.OnFiltersChanged -= UserSettingsManager_OnFiltersChanged;
        }

        private void CreateItemUI_OnFavoriteToggled(Item item, bool value)
        {
            bool contains = UserSettingsManager.Instance.GetFilterFavoriteValue(item.id);

            if (contains && value == false)
            {
                RemoveFavoriteItem(item);
            }
            else if (!contains && value == true)
            {
                favoriteItems.Add(item);
                UserSettingsManager.Instance.AddFavorite(item.id);
            }
            else if (contains)
            {
                favoriteItems.Add(item);
            }
        }

        private void UserSettingsManager_OnFiltersChanged()
        {
            favoriteItems.Clear();

            Item[] items = GameManager.Instance.GetAllItems();
            List<int> favorites = UserSettingsManager.Instance.GetFavorites();

            IEnumerable<Item> sharedItems = items.Where(item => favorites.Contains(item.id));

            foreach (Item item in sharedItems)
            {
                favoriteItems.Add(item);
            }
        }

        public void Instantiate()
        {
            foreach (Item item in favoriteItems)
            {
                Transform favoriteItemUITransform = Instantiate(template, container);
                favoriteItemUITransform.gameObject.SetActive(true);

                CreateFavoriteUI createFavoriteUI = favoriteItemUITransform.GetComponent<CreateFavoriteUI>();
                createFavoriteUI.Instantiate(item);
            }
        }

        // Had a problem with direct removing so I did this
        public void RemoveFavoriteItem(Item itemToRemove)
        {
            Item item = favoriteItems.FirstOrDefault(a => a.id == itemToRemove.id);

            favoriteItems.Remove(item);

            UserSettingsManager.Instance.RemoveFavorite(itemToRemove.id);
        }
    }
}
