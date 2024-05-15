using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class LanguageUI : MonoBehaviour
    {
        private readonly string LANGUAGE = "language";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI languageNameText;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Transform container;
        [SerializeField] private Transform template;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private SettingsUI settingsUI;

        private void Awake()
        {
            confirmButton.onClick.AddListener(() =>
            {
                LocaleSelector.Instance.SaveDefaultLocale();
                settingsUI.ToggleObjects(false);
                settingsUI.LocalizeHeaderText();
            });

            cancelButton.onClick.AddListener(() =>
            {
                LocaleSelector.Instance.Cancel();
                settingsUI.ToggleObjects(false);
            });

            template.gameObject.SetActive(false);
        }

        private void Start()
        {
            LocalizedString localizedString = languageNameText.GetComponent<LocalizeStringEvent>().StringReference;
            localizedString.SetReference(LocaleSelector.Instance.STRING_TABLE, LocalizationSettings.SelectedLocale.LocaleName);
        }

        private void OnDestroy()
        {
            confirmButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
        }

        public void Instantiate()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, LANGUAGE);
            scrollRect.gameObject.SetActive(true);

            if (container.childCount > 1) return;

            int localeId = 0;
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                Transform languageUITransform = Instantiate(template, container);
                languageUITransform.gameObject.SetActive(true);

                CreateLanguageUI createLanguageUI = languageUITransform.GetComponent<CreateLanguageUI>();
                createLanguageUI.Instantiate(locale, localeId);

                localeId++;
            }
        }
    }
}
