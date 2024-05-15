using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class SettingsUI : MonoBehaviour
    {
        private readonly string SETTINGS = "Settings";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Button languageSettingsButton;

        private void Awake()
        {
            languageSettingsButton.onClick.AddListener(() => { });
        }

        private void OnEnable()
        {
            headerText.text = SETTINGS;
        }

        private void OnDestroy()
        {
            languageSettingsButton.onClick.RemoveAllListeners();
        }
    }
}
