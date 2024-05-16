using ExordiumGamesAssignment.Scripts.Api.Models;
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
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;

        private AuthenticationResponse authenticationResponse;

        private void Awake()
        {
            registerButton.onClick.AddListener(() =>
            {
                if (emailInputField.text.Length < 6 && passwordInputField.text.Length <= 1) return;

                StartCoroutine(GameManager.Instance.Register((authenticationResponse) =>
                {
                    this.authenticationResponse = authenticationResponse;
                }, 
                new User(emailInputField.text, passwordInputField.text)));
            });

            loginButton.onClick.AddListener(() =>
            {
                if (emailInputField.text.Length < 6 && passwordInputField.text.Length <= 1) return;

                StartCoroutine(GameManager.Instance.Login((authenticationResponse) =>
                {
                    this.authenticationResponse = authenticationResponse;
                },
                new User(emailInputField.text, passwordInputField.text)));
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
