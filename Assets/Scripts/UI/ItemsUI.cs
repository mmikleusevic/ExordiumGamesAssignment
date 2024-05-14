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
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private Transform container;
        [SerializeField] private Transform template;
        [SerializeField] private ScrollRect scrollRect;

        private bool isLoading = false;
        private int pageNumber = 1;

        private void Awake()
        {
            headerText.text = gameObject.name;
            template.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnDisable()
        {
            foreach (Transform child in container)
            {
                if (child == template) continue;
                Destroy(child.gameObject);
            }

            pageNumber = 1;

            scrollRect.onValueChanged.RemoveAllListeners();
        }

        public IEnumerator Instantiate()
        {
            Item[] currentItems = null;
            yield return StartCoroutine(GameManager.Instance.LoadItems((items) => currentItems = items,pageNumber));

            if (currentItems == null) yield break;

            foreach (Item item in currentItems)
            {
                Transform itemUITransform = Instantiate(template, container);
                itemUITransform.gameObject.SetActive(true);

                CreateItemUI createItemUI = itemUITransform.GetComponent<CreateItemUI>();
                createItemUI.Instantiate(item);
            }

            pageNumber++;

            yield return new WaitForSeconds(1f);
            isLoading = false;
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
