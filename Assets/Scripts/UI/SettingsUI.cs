using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class SettingsUI : MonoBehaviour
    {
        private readonly string SETTINGS = "settings";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Button languageSettingsButton;
        [SerializeField] private Button themeSettingsButton;
        [SerializeField] private LanguageUI languageUI;
        [SerializeField] private ThemeUI themeUI;


        private void Awake()
        {
            languageSettingsButton.onClick.AddListener(() =>
            {
                languageUI.Instantiate();
            });

            themeSettingsButton.onClick.AddListener(() =>
            {
                themeUI.Instantiate();
            });

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            LocalizeHeaderText();
        }

        private void OnDestroy()
        {
            languageSettingsButton.onClick.RemoveAllListeners();
        }

        public void LocalizeHeaderText()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, SETTINGS);
        }
    }
}
