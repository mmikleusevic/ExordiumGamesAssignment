using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateCategoryUI : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private TextMeshProUGUI itemCategoryText;

        private int id;

        public void Instantiate(ItemCategory itemCategory)
        {
            id = itemCategory.id;

            toggle.onValueChanged.AddListener((value) =>
            {
                UserSettingsManager.Instance.UpdateFilterCategory(id, value);
            });

            bool value = UserSettingsManager.Instance.GetFilterCategoryValue(id);
            toggle.isOn = value;

            itemCategoryText.text = itemCategory.name;

            ThemeManager.Instance.AddTextElement(itemCategoryText);
            ThemeManager.Instance.AddToggle(toggle);
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
