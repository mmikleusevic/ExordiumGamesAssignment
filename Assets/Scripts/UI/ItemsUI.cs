using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class ItemsUI : MonoBehaviour
    {
        private readonly string ITEMS = "Items";

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Transform container;
        [SerializeField] private Transform template;
        [SerializeField] private ScrollRect scrollRect;

        private bool isLoading = false;

        private void Awake()
        {
            scrollRect.gameObject.SetActive(false);
            template.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnDisable()
        {
            scrollRect.onValueChanged.RemoveAllListeners();
        }

        public IEnumerator Instantiate()
        {
            headerText.text = ITEMS;
            Item[] newItems = null;

            yield return StartCoroutine(GameManager.Instance.LoadItems((items) => newItems = items));

            if (newItems == null)
            {
                FilterItems();
                yield break;
            }

            foreach (Item item in newItems)
            {
                Transform itemUITransform = Instantiate(template, container);
                itemUITransform.gameObject.SetActive(true);

                CreateItemUI createItemUI = itemUITransform.GetComponent<CreateItemUI>();
                createItemUI.Instantiate(item);
            }

            yield return new WaitForSeconds(1f);
            isLoading = false;
        }

        public void FilterItems()
        {
            foreach (Transform child in container)
            {
                if (child == template) continue;

                CreateItemUI createItemUI = child.GetComponent<CreateItemUI>();
                if (GameManager.Instance.GetFilterCategoryValue(createItemUI.categoryId) && GameManager.Instance.GetFilterRetailerValue(createItemUI.retailerId))
                {
                    createItemUI.gameObject.SetActive(true);
                }
                else
                {
                    createItemUI.gameObject.SetActive(false);
                }
            }
        }

        private void OnScrollValueChanged(Vector2 scrollPosition)
        {
            if (!isLoading && scrollPosition.y <= 0.0f)
            {
                isLoading = true;
                StartCoroutine(Instantiate());
            }
        }
    }
}
