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
        [SerializeField] private Button logoutButton;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TextMeshProUGUI emailText;
        [SerializeField] private TextMeshProUGUI passwordText;
        [SerializeField] private TextMeshProUGUI usernameText;
        [SerializeField] private CreateAccountUI createAccountUI;

        private int minEmailLength = 6;
        private int minPasswordLength = 1;

        private void Awake()
        {
            registerButton.onClick.AddListener(() =>
            {
                if (emailInputField.text.Length < minEmailLength || passwordInputField.text.Length < minPasswordLength) return;

                StartCoroutine(GameManager.Instance.Register(HandleRegisterResponse, new User(emailInputField.text, passwordInputField.text)));
            });

            loginButton.onClick.AddListener(() =>
            {
                if (emailInputField.text.Length < minEmailLength || passwordInputField.text.Length < minPasswordLength) return;

                StartCoroutine(GameManager.Instance.Login(HandleLoginResponse, new User(emailInputField.text, passwordInputField.text)));
            });

            logoutButton.onClick.AddListener(() => Logout());

            logoutButton.gameObject.SetActive(false);
            usernameText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            createAccountUI.OnLoginSuccess += CreateAccountUI_OnLoginSuccess;

            LocalizeHeaderText();
        }

        private void OnDisable()
        {
            createAccountUI.OnLoginSuccess -= CreateAccountUI_OnLoginSuccess;
        }

        private void OnDestroy()
        {
            registerButton.onClick.RemoveAllListeners();
            loginButton.onClick.RemoveAllListeners();
            logoutButton.onClick.RemoveAllListeners();
        }

        private void CreateAccountUI_OnLoginSuccess()
        {
            usernameText.gameObject.SetActive(true);
            usernameText.text = emailInputField.text;

            registerButton.gameObject.SetActive(false);
            loginButton.gameObject.SetActive(false);
            emailInputField.gameObject.SetActive(false);
            passwordInputField.gameObject.SetActive(false);
            emailText.gameObject.SetActive(false);
            passwordText.gameObject.SetActive(false);
            logoutButton.gameObject.SetActive(true);

            UserSettingsManager.Instance.LoadUserSettings(emailInputField.text);
        }

        public void LocalizeHeaderText()
        {
            headerText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, ACCOUNT);
        }

        private void HandleRegisterResponse(AuthenticationResponse authenticationResponse)
        {
            if (authenticationResponse == null) return;

            if (authenticationResponse.isSuccessful == false)
            {
                createAccountUI.Instantiate(authenticationResponse.message, AuthResponse.REGISTER_FAILURE);
            }
            else
            {
                createAccountUI.Instantiate(authenticationResponse.message, AuthResponse.REGISTER_SUCCESS);
            }
        }

        private void HandleLoginResponse(AuthenticationResponse authenticationResponse)
        {
            if (authenticationResponse == null) return;

            if (authenticationResponse.isSuccessful == false)
            {
                createAccountUI.Instantiate(authenticationResponse.message, AuthResponse.LOGIN_FAILURE);
            }
            else
            {
                createAccountUI.Instantiate(authenticationResponse.message, AuthResponse.LOGIN_SUCCESS);
            }
        }

        private void Logout()
        {
            UserSettingsManager.Instance.LoadDefaultSettings();

            registerButton.gameObject.SetActive(true);
            loginButton.gameObject.SetActive(true);
            emailInputField.gameObject.SetActive(true);
            passwordInputField.gameObject.SetActive(true);
            emailText.gameObject.SetActive(true);
            passwordText.gameObject.SetActive(true);

            logoutButton.gameObject.SetActive(false);
            usernameText.gameObject.SetActive(false);
        }
    }
}
