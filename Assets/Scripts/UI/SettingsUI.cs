using ExordiumGamesAssignment.Scripts.Game;
using System.Collections.Generic;
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
        [SerializeField] private List<GameObject> gameObjectsToEnable;
        [SerializeField] private LanguageUI languageUI;

        private void Awake()
        {
            languageSettingsButton.onClick.AddListener(() =>
            {
                ToggleObjects(true);
                languageUI.Instantiate();
            });
        }

        private void OnEnable()
        {
            LocalizeHeaderText();
        }

        private void OnDestroy()
        {
            languageSettingsButton.onClick.RemoveAllListeners();
        }

        public void ToggleObjects(bool value)
        {
            foreach (GameObject gObject in gameObjectsToEnable)
            {
                gObject.SetActive(value);
            }
        }

        public void LocalizeHeaderText()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, SETTINGS);
        }
    }
}
