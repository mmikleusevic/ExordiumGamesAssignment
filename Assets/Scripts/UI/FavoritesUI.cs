using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class FavoritesUI : ItemsUI
    {
        private readonly string FAVORITES = "favorites";

        public override IEnumerator Instantiate()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, FAVORITES);
            Item[] newItems = null;

            yield return StartCoroutine(GameManager.Instance.LoadItems((items) => newItems = items));

            if (newItems == null) yield break;

            List<int> favoriteIds = UserSettingsManager.Instance.GetFavorites();
            List<Item> favorites = newItems.Where(item => favoriteIds.Contains(item.id)).ToList();

            foreach (Item item in newItems)
            {
                Transform itemUITransform = Instantiate(template, container);
                itemUITransform.gameObject.SetActive(true);

                CreateItemUI createItemUI = itemUITransform.GetComponent<CreateItemUI>();
                createItemUI.Instantiate(item);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
