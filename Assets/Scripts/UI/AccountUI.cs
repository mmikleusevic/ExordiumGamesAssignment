using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class AccountUI : MonoBehaviour
    {
        private readonly string ACCOUNT = "account";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button loginButton;

        private void Awake()
        {
            registerButton.onClick.AddListener(() =>
            {

            });

            loginButton.onClick.AddListener(() =>
            {

            });

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            LocalizeHeaderText();
        }

        private void OnDestroy()
        {
            registerButton.onClick.RemoveAllListeners();
            loginButton.onClick.RemoveAllListeners();
        }

        public void LocalizeHeaderText()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, ACCOUNT);
        }
    }
}
