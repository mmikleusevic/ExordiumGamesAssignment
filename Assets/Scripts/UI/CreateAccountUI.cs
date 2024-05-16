using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateAccountUI : MonoBehaviour
    {
        private readonly string RETRY = "Retry";
        private readonly string CONTINUE = "Continue";

        public event Action OnLoginSuccess;

        [SerializeField] private Button messageButton;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private List<GameObject> gameObjectsToToggle;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Instantiate(string message, AuthResponse authResponse)
        {
            gameObject.SetActive(true);
            ToggleObjects(true);

            messageButton.onClick.RemoveAllListeners();

            HandleResponse(message, authResponse);
        }

        private void HandleResponse(string message, AuthResponse authResponse)
        {
            TextMeshProUGUI messageButtonText = messageButton.GetComponentInChildren<TextMeshProUGUI>();
            messageText.text = message;

            switch (authResponse)
            {
                case AuthResponse.REGISTER_SUCCESS:
                    messageButton.onClick.AddListener(() =>
                    {
                        ToggleObjects(false);
                    });
                    messageText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, AuthenticationResponse.ACCOUNT_CREATED);
                    messageButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, CONTINUE);
                    break;
                case AuthResponse.REGISTER_FAILURE:
                    messageButton.onClick.AddListener(() =>
                    {
                        ToggleObjects(false);
                    });
                    messageText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, AuthenticationResponse.EMAIL_TAKEN);
                    messageButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, RETRY);
                    break;
                case AuthResponse.LOGIN_SUCCESS:
                    messageButton.onClick.AddListener(() =>
                    {
                        OnLoginSuccess?.Invoke();
                        ToggleObjects(false);
                    });
                    messageText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, AuthenticationResponse.WELCOME);
                    messageButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, CONTINUE);
                    break;
                case AuthResponse.LOGIN_FAILURE:
                    messageButton.onClick.AddListener(() =>
                    {
                        ToggleObjects(false);
                    });
                    messageText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, AuthenticationResponse.INVALID_EMAIL_OR_PASSWORD);
                    messageButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(LocaleSelector.Instance.STRING_TABLE, RETRY);
                    break;
            }
        }

        public void ToggleObjects(bool value)
        {
            foreach (GameObject gObject in gameObjectsToToggle)
            {
                gObject.SetActive(value);
            }
        }
    }
}
