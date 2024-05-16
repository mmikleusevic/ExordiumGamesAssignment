using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateLanguageUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI languageNameText;
        [SerializeField] private Toggle languageToggle;
        [SerializeField] private Image image;

        private Locale locale;

        private void OnEnable()
        {
            if (locale != null && LocalizationSettings.SelectedLocale == locale) languageToggle.Select();
        }

        public void Instantiate(Locale locale, int localeId)
        {
            this.locale = locale;

            LocalizedString localizedString = languageNameText.GetComponent<LocalizeStringEvent>().StringReference;
            localizedString.SetReference(LocaleSelector.Instance.STRING_TABLE, locale.LocaleName);

            languageToggle.onValueChanged.AddListener((value) =>
            {
                LocaleSelector.Instance.ChangeLocale(localeId);
            });

            if (LocalizationSettings.SelectedLocale == locale) languageToggle.Select();

            LocalizedAsset<Sprite> localizedAsset = image.GetComponent<LocalizedAssetEvent<Sprite, LocalizedSprite, UnityEventSprite>>().AssetReference;
            localizedAsset.SetReference(LocaleSelector.Instance.ASSET_TABLE, locale.LocaleName);

            ThemeManager.Instance.AddTextElement(languageNameText);
            ThemeManager.Instance.AddToggle(languageToggle);
            ThemeManager.Instance.AddOutline(languageNameText.GetComponentInParent<Outline>());
        }
    }
}
