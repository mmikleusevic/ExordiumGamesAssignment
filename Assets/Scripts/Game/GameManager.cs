using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Api.Services;
using ExordiumGamesAssignment.Scripts.UI;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject fetchDataUI;
        [SerializeField] private MainUI mainUI;

        private RetailerServiceHandler retailerServiceHandler;
        private ItemCategoriesServiceHandler itemCategoriesServiceHandler;
        private ItemServiceHandler itemServiceHandler;
        private ItemCategory[] itemCategories;
        private Item[] items;
        private Retailer[] retailers;

        private void Awake()
        {
            Instance = this;

            mainUI.gameObject.SetActive(false);
            fetchDataUI.SetActive(true);

            itemCategoriesServiceHandler = new ItemCategoriesServiceHandler();
            itemServiceHandler = new ItemServiceHandler();
            retailerServiceHandler = new RetailerServiceHandler();
        }

        private IEnumerator Start()
        {
            yield return itemCategoriesServiceHandler.GetItemCategories((itemCategories) => this.itemCategories = itemCategories);
            yield return itemServiceHandler.GetItems((items) => this.items = items, 1);
            yield return retailerServiceHandler.GetRetailers((retailers) => this.retailers = retailers);

            mainUI.gameObject.SetActive(true);
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

        public Retailer GetRetailer(int index)
        {
            return retailers.FirstOrDefault(a => a.id == index);
        }

        public ItemCategory[] GetItemCategories()
        {
            return itemCategories;
        }

        public ItemCategory GetItemCategory(int index)
        {
            return itemCategories.FirstOrDefault(a => a.id == index);
        }

        public IEnumerator LoadItems(Action<Item[]> callback, int pageNumber)
        {
            yield return itemServiceHandler.GetItems((items) => this.items = items, pageNumber);
            callback?.Invoke(items);
        }
    }
}
