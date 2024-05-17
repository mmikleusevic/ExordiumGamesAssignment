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

        [SerializeField] protected TextMeshProUGUI headerText;
        [SerializeField] protected Transform container;
        [SerializeField] protected Transform template;
        [SerializeField] protected ScrollRect scrollRect;

        private bool isLoading = false;

        private void Awake()
        {
            scrollRect.gameObject.SetActive(false);
            template.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnDisable()
        {
            scrollRect.verticalNormalizedPosition = 1;
            scrollRect.onValueChanged.RemoveAllListeners();
        }

        public virtual IEnumerator Instantiate()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, ITEMS);
            Item[] newItems = null;

            yield return StartCoroutine(GameManager.Instance.LoadItems((items) => newItems = items));

            if (newItems == null)
            {
                FilterItems();
                yield break;
            }

            foreach (Item item in newItems)
            {
                Transform itemUITransform = Instantiate(template, container);
                itemUITransform.gameObject.SetActive(true);

                CreateItemUI createItemUI = itemUITransform.GetComponent<CreateItemUI>();
                createItemUI.Instantiate(item);
            }

            yield return new WaitForSeconds(1f);
            isLoading = false;
        }

        public void FilterItems()
        {
            foreach (Transform child in container)
            {
                if (child == template) continue;

                CreateItemUI createItemUI = child.GetComponent<CreateItemUI>();
                if (UserSettingsManager.Instance.GetFilterCategoryValue(createItemUI.categoryId) && UserSettingsManager.Instance.GetFilterRetailerValue(createItemUI.retailerId))
                {
                    createItemUI.gameObject.SetActive(true);
                }
                else
                {
                    createItemUI.gameObject.SetActive(false);
                }
            }
        }

        private void OnScrollValueChanged(Vector2 scrollPosition)
        {
            if (!isLoading && scrollPosition.y <= 0.0f)
            {
                isLoading = true;
                StartCoroutine(Instantiate());
            }
        }
    }
}
