using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class RetailerUI : MonoBehaviour
    {
        private readonly string RETAILER = "retailer";

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
            UserSettingsManager.Instance?.SaveSelectedUserRetailersSettings();
        }

        private void OnDestroy()
        {
            UserSettingsManager.Instance.OnFiltersChanged -= UserSettingsManager_OnFiltersChanged;
        }

        private void UserSettingsManager_OnFiltersChanged()
        {
            Retailer[] retailers = GameManager.Instance.GetRetailers();

            int index = 0;

            foreach (Transform child in container)
            {
                if (child == template) continue;

                int id = retailers[index].id;

                bool value = UserSettingsManager.Instance.GetFilterRetailerValue(id);
                child.GetComponent<CreateRetailerUI>().UpdateToggle(value);

                index++;
            }
        }

        public void Instantiate()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, RETAILER);
            scrollRect.gameObject.SetActive(true);

            if (container.childCount > 1) return;

            Retailer[] retailers = GameManager.Instance.GetRetailers();

            foreach (Retailer retailer in retailers)
            {
                Transform retailerUITransform = Instantiate(template, container);
                retailerUITransform.gameObject.SetActive(true);

                CreateRetailerUI createRetailerUI = retailerUITransform.GetComponent<CreateRetailerUI>();
                createRetailerUI.Instantiate(retailer);
            }
        }
    }
}
