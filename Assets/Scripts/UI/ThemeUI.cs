using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class ThemeUI : MonoBehaviour
    {
        private readonly string THEME = "theme";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI themeNameText;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private GameObject[] gameObjectsToToggle;

        private void Awake()
        {
            confirmButton.onClick.AddListener(() =>
            {
                ThemeManager.Instance.SaveDefaultTheme();
                ToggleObjects(false);
            });

            cancelButton.onClick.AddListener(() =>
            {
                ThemeManager.Instance.Cancel();
                ToggleObjects(false);
            });

            ToggleObjects(false);
        }

        private void Start()
        {
            LocalizedString localizedString = themeNameText.GetComponent<LocalizeStringEvent>().StringReference;
            localizedString.SetReference(LocaleSelector.Instance.STRING_TABLE, ThemeManager.Instance.CurrentTheme.ToString().ToLower());
        }

        private void OnDestroy()
        {
            confirmButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
        }

        public void Instantiate()
        {
            ToggleObjects(true);

            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, THEME);
        }

        private void ToggleObjects(bool value)
        {
            foreach (GameObject gObject in gameObjectsToToggle)
            {
                gObject.SetActive(value);
            }
        }
    }
}
