using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Api.Services;
using ExordiumGamesAssignment.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        private readonly string CATEGORY_ID = "categoryId";

        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject fetchDataUI;
        [SerializeField] private MainUI mainUI;

        private RetailerServiceHandler retailerServiceHandler;
        private ItemCategoriesServiceHandler itemCategoriesServiceHandler;
        private ItemServiceHandler itemServiceHandler;
        private ItemCategory[] itemCategories;
        private Item[] items;
        private Retailer[] retailers;
        private Dictionary<int, bool> filterCategories;

        private void Awake()
        {
            Instance = this;

            mainUI.gameObject.SetActive(false);
            fetchDataUI.SetActive(true);

            itemCategoriesServiceHandler = new ItemCategoriesServiceHandler();
            itemServiceHandler = new ItemServiceHandler();
            retailerServiceHandler = new RetailerServiceHandler();
            filterCategories = new Dictionary<int, bool>();
        }

        private IEnumerator Start()
        {
            yield return itemCategoriesServiceHandler.GetItemCategories((itemCategories) => this.itemCategories = itemCategories);
            yield return SetFilterCategories();
            yield return retailerServiceHandler.GetRetailers((retailers) => this.retailers = retailers);

            mainUI.gameObject.SetActive(true);
            fetchDataUI.SetActive(false);
            mainUI.TriggerItems();
        }

        private IEnumerator SetFilterCategories()
        {
            foreach (var category in itemCategories)
            {
                if (!PlayerPrefs.HasKey(CATEGORY_ID + category.id))
                {
                    PlayerPrefs.SetString(CATEGORY_ID + category.id, "true");
                }

                string stringValue = PlayerPrefs.GetString(CATEGORY_ID + category.id);
                bool.TryParse(stringValue, out bool value);

                filterCategories.Add(category.id, value);

                yield return null;
            }
        }

        public void UpdateFilterCategory(int id, bool value)
        {
            PlayerPrefs.SetString(CATEGORY_ID + id, value.ToString());
            filterCategories[id] = value;
        }

        public Dictionary<int, bool> GetFilterCategories()
        {
            return filterCategories;
        }

        public bool GetFilterCategoryValue(int id)
        {
            return filterCategories[id];
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
