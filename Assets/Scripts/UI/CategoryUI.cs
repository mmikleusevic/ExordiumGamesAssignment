using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CategoryUI : MonoBehaviour
    {
        private readonly string CATEGORY = "category";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Transform container;
        [SerializeField] private Transform template;
        [SerializeField] private ScrollRect scrollRect;

        private void Awake()
        {
            template.gameObject.SetActive(false);
            scrollRect.gameObject.SetActive(false);
        }

        private void Start()
        {
            UserSettingsManager.Instance.OnFiltersChanged += UserSettingsManager_OnFiltersChanged;

            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            scrollRect.verticalNormalizedPosition = 1;
            UserSettingsManager.Instance?.SaveSelectedUserCategoriesSettings();
        }

        private void OnDestroy()
        {
            UserSettingsManager.Instance.OnFiltersChanged -= UserSettingsManager_OnFiltersChanged;
        }

        private void UserSettingsManager_OnFiltersChanged()
        {
            ItemCategory[] itemCategories = GameManager.Instance.GetItemCategories();

            int index = 0;
            foreach (Transform child in container)
            {
                if (child == template) continue;

                int id = itemCategories[index].id;

                bool value = UserSettingsManager.Instance.GetFilterCategoryValue(id);
                child.GetComponent<CreateCategoryUI>().UpdateToggle(value);

                index++;
            }
        }

        public void Instantiate()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, CATEGORY);
            scrollRect.gameObject.SetActive(true);

            if (container.childCount > 1) return;

            ItemCategory[] itemCategories = GameManager.Instance.GetItemCategories();

            foreach (ItemCategory itemCategory in itemCategories)
            {
                Transform itemCategoryTransform = Instantiate(template, container);
                itemCategoryTransform.gameObject.SetActive(true);

                CreateCategoryUI createCategoryUI = itemCategoryTransform.GetComponent<CreateCategoryUI>();
                createCategoryUI.Instantiate(itemCategory);
            }
        }
    }
}
