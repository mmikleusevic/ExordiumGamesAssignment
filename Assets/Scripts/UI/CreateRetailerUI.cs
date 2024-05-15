using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class CreateRetailerUI : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private TextMeshProUGUI retailerNameText;
        [SerializeField] private ImageLoader imageLoader;

        private int id;

        public void Instantiate(Retailer retailer)
        {
            StartCoroutine(imageLoader.LoadImageFromUrl(retailer.image_url));
            id = retailer.id;

            toggle.onValueChanged.AddListener((value) =>
            {
                GameManager.Instance.UpdateFilterRetailer(id, value);
                PlayerPrefs.Save();
            });

            bool value = GameManager.Instance.GetFilterRetailerValue(id);
            toggle.isOn = value;

            retailerNameText.text = retailer.name;
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
