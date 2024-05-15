using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private Button itemsButton;
        [SerializeField] private Button categoryButton;
        [SerializeField] private Button accountButton;
        [SerializeField] private Button retailerButton;
        [SerializeField] private Button favoritesButton;
        [SerializeField] private Button settingsButton;

        [SerializeField] private ItemsUI itemsUI;
        [SerializeField] private CategoryUI categoryUI;
        [SerializeField] private RetailerUI retailerUI;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private GameObject bottomUIPart;
        private Button activeButton;

        private void Awake()
        {
            itemsButton.onClick.AddListener(() =>
            {
                DisableCurrentButtonEnableLast(itemsButton);
                bottomUIPart.gameObject.SetActive(true);
                uIManager.ActivateUIElement(itemsUI);
                StartCoroutine(itemsUI.Instantiate());
            });

            categoryButton.onClick.AddListener(() =>
            {
                DisableCurrentButtonEnableLast(categoryButton);
                bottomUIPart.gameObject.SetActive(true);
                uIManager.ActivateUIElement(categoryUI);
                categoryUI.Instantiate();
            });

            accountButton.onClick.AddListener(() =>
            {
                DisableCurrentButtonEnableLast(accountButton);
            });

            retailerButton.onClick.AddListener(() =>
            {
                DisableCurrentButtonEnableLast(retailerButton);
                bottomUIPart.gameObject.SetActive(true);
                uIManager.ActivateUIElement(retailerUI);
                retailerUI.Instantiate();
            });

            favoritesButton.onClick.AddListener(() =>
            {
                DisableCurrentButtonEnableLast(favoritesButton);
            });

            settingsButton.onClick.AddListener(() =>
            {
                DisableCurrentButtonEnableLast(settingsButton);
            });

            bottomUIPart.gameObject.SetActive(false);
        }

        public void TriggerItems()
        {
            itemsButton.onClick.Invoke();
        }

        private void DisableCurrentButtonEnableLast(Button newActiveButton)
        {
            if (activeButton != null)
            {
                activeButton.interactable = true;
            }

            activeButton = newActiveButton;
            activeButton.interactable = false;
        }

        private void OnDestroy()
        {
            itemsButton.onClick.RemoveAllListeners();
            categoryButton.onClick.RemoveAllListeners();
            accountButton.onClick.RemoveAllListeners();
            retailerButton.onClick.RemoveAllListeners();
            favoritesButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
        }
    }
}
