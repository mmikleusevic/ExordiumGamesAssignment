using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CategoryUI : MonoBehaviour
    {
        private readonly string CATEGORY = "Category";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Transform container;
        [SerializeField] private Transform template;
        [SerializeField] private ScrollRect scrollRect;

        private void Awake()
        {
            template.gameObject.SetActive(false);
            scrollRect.gameObject.SetActive(false);
        }

        public void Instantiate()
        {
            headerText.text = CATEGORY;
            scrollRect.gameObject.SetActive(true);

            if (container.childCount > 1) return;

            ItemCategory[] itemCategories = GameManager.Instance.GetItemCategories();

            foreach (ItemCategory itemCategory in itemCategories)
            {
                Transform itemCategoryTransform = Instantiate(template, container);
                itemCategoryTransform.gameObject.SetActive(true);

                CreateCategoryUI createCategoryUI = itemCategoryTransform.GetComponent<CreateCategoryUI>();
                createCategoryUI.Instantiate(itemCategory);
            }
        }
    }
}