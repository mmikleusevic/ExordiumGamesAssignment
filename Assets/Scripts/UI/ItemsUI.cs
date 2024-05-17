using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class ItemsUI : MonoBehaviour
    {
        private readonly string ITEMS = "items";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Transform container;
        [SerializeField] private Transform template;
        [SerializeField] private ScrollRect scrollRect;

        private bool isLoading = false;
        private bool load = true;

        private void Awake()
        {
            scrollRect.gameObject.SetActive(false);
            template.gameObject.SetActive(false);

            gameObject.SetActive(false);
        }

        private void Start()
        {
            CreateFavoriteUI.OnFavoriteRemoved += CreateFavoriteUI_OnFavoriteRemoved;
            UserSettingsManager.Instance.OnFiltersChanged += UserSettingsManager_OnFiltersChanged;
        }

        private void OnEnable()
        {
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnDisable()
        {
            scrollRect.verticalNormalizedPosition = 1;
            scrollRect.onValueChanged.RemoveAllListeners();
            UserSettingsManager.Instance?.SaveSelectedUserFavoritesSettings();
        }

        private void OnDestroy()
        {
            CreateFavoriteUI.OnFavoriteRemoved -= CreateFavoriteUI_OnFavoriteRemoved;
            UserSettingsManager.Instance.OnFiltersChanged -= UserSettingsManager_OnFiltersChanged;
        }

        private void CreateFavoriteUI_OnFavoriteRemoved(Item item)
        {
            foreach (Transform child in container)
            {
                if (child == template) continue;

                CreateItemUI createItemUI = child.GetComponent<CreateItemUI>();
                Item thisItem = createItemUI.GetItem();

                if (thisItem == item)
                {
                    createItemUI.ToggleItem();
                }
            }
        }

        private void UserSettingsManager_OnFiltersChanged()
        {
            foreach (Transform child in container)
            {
                if (child == template) continue;

                CreateItemUI createItemUI = child.GetComponent<CreateItemUI>();
                Item item = createItemUI.GetItem();

                FilterFavorites(createItemUI, item.id);
            }
        }

        public virtual IEnumerator Instantiate()
        {
            scrollRect.gameObject.SetActive(true);
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, ITEMS);

            FilterItems();

            if (!load) yield break;

            isLoading = true;

            Item[] newItems = null;
            yield return StartCoroutine(GameManager.Instance.LoadItems((items) => newItems = items));

            if (newItems == null) yield break;

            foreach (Item item in newItems)
            {
                Transform itemUITransform = Instantiate(template, container);
                itemUITransform.gameObject.SetActive(true);

                CreateItemUI createItemUI = itemUITransform.GetComponent<CreateItemUI>();
                createItemUI.Instantiate(item);

                FilterFavorites(createItemUI, item.id);
            }

            yield return new WaitForSeconds(1f);
            isLoading = false;
            load = false;
        }

        public void FilterItems()
        {
            foreach (Transform child in container)
            {
                if (child == template) continue;

                CreateItemUI createItemUI = child.GetComponent<CreateItemUI>();
                Item item = createItemUI.GetItem();

                if (UserSettingsManager.Instance.GetFilterCategoryValue(item.item_category_id) && UserSettingsManager.Instance.GetFilterRetailerValue(item.retailer_id))
                {
                    createItemUI.gameObject.SetActive(true);
                }
                else
                {
                    createItemUI.gameObject.SetActive(false);
                }
            }
        }

        private void FilterFavorites(CreateItemUI createItemUI, int id)
        {
            if (UserSettingsManager.Instance.GetFilterFavoriteValue(id) != createItemUI.GetToggleValue())
            {
                createItemUI.ToggleItem();
            }
        }

        private void OnScrollValueChanged(Vector2 scrollPosition)
        {
            if (!isLoading && scrollPosition.y <= 0.0f)
            {
                load = true;
                StartCoroutine(Instantiate());
            }
        }
    }
}
