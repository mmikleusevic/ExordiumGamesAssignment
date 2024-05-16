using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateThemeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI themeNameText;
        [SerializeField] private Toggle themeToggle;
        [SerializeField] private Theme theme;

        private void Start()
        {
            Instantiate();
        }

        private void OnEnable()
        {
            if (ThemeManager.Instance != null && ThemeManager.Instance.CurrentTheme == theme) themeToggle.Select();
        }

        public void Instantiate()
        {
            LocalizedString localizedString = themeNameText.GetComponent<LocalizeStringEvent>().StringReference;
            localizedString.SetReference(LocaleSelector.Instance.STRING_TABLE, theme.ToString().ToLower() + "Theme");

            themeToggle.onValueChanged.AddListener((value) =>
            {
                ThemeManager.Instance.SetTheme(theme);
            });
        }
    }
}
