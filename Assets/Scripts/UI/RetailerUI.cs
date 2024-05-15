using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class RetailerUI : MonoBehaviour
    {
        private readonly string RETAILER = "Retailer";

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
            headerText.text = RETAILER;
            scrollRect.gameObject.SetActive(true);

            if (container.childCount > 1) return;

            Retailer[] retailers = GameManager.Instance.GetRetailers();

            foreach (Retailer retailer in retailers)
            {
                Transform retailerUITransform = Instantiate(template, container);
                retailerUITransform.gameObject.SetActive(true);

                CreateRetailerUI createRetailerUI = retailerUITransform.GetComponent<CreateRetailerUI>();
                createRetailerUI.Instantiate(retailer);
            }
        }
    }
}