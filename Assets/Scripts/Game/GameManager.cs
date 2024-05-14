using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Api.Services;
using System.Collections;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject fetchDataUI;

        private RetailerServiceHandler retailerServiceHandler;
        private ItemCategoriesServiceHandler itemCategoriesServiceHandler;
        private ItemServiceHandler itemServiceHandler;
        private ItemCategory[] itemCategories;
        private Item[] items;
        private Retailer[] retailers;

        private void Awake()
        {
            Instance = this;

            fetchDataUI.SetActive(true);

            itemCategoriesServiceHandler = new ItemCategoriesServiceHandler();
            itemServiceHandler = new ItemServiceHandler();
            retailerServiceHandler = new RetailerServiceHandler();
        }

        private IEnumerator Start()
        {
            yield return itemCategoriesServiceHandler.GetItemCategories((itemCategories) => this.itemCategories = itemCategories);
            yield return itemServiceHandler.GetItems((items) => this.items = items);
            yield return retailerServiceHandler.GetRetailers((retailers) => this.retailers = retailers);

            fetchDataUI.SetActive(false);
        }

        public Item[] GetItems()
        {
            return items;
        }

        public Retailer[] GetRetailers()
        {
            return retailers;
        }

        public ItemCategory[] GetItemCategories()
        {
            return itemCategories;
        }
    }
}
