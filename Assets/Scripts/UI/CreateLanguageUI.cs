using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Events;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateLanguageUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI languageNameText;
        [SerializeField] private Button languageButton;
        [SerializeField] private Image image;

        public void Instantiate(Locale locale, int localeId)
        {
            LocalizedString localizedString = languageNameText.GetComponent<LocalizeStringEvent>().StringReference;
            localizedString.SetReference(LocaleSelector.Instance.STRING_TABLE, locale.LocaleName);

            languageButton.onClick.AddListener(() =>
            {
                LocaleSelector.Instance.ChangeLocale(localeId);
            });

            LocalizedAsset<Sprite> localizedAsset = image.GetComponent<LocalizedAssetEvent<Sprite, LocalizedSprite, UnityEventSprite>>().AssetReference;
            localizedAsset.SetReference(LocaleSelector.Instance.ASSET_TABLE, locale.LocaleName);
        }
    }
}
