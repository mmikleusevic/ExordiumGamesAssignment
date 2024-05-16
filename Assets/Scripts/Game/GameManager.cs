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
        private readonly string RETAILER_ID = "retailerId";

        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject fetchDataUI;
        [SerializeField] private MainUI mainUI;

        private RetailerServiceHandler retailerServiceHandler;
        private ItemCategoriesServiceHandler itemCategoriesServiceHandler;
        private ItemServiceHandler itemServiceHandler;
        private UserServiceHandler userServiceHandler;

        private ItemCategory[] itemCategories;
        private Item[] items;
        private Retailer[] retailers;

        private Dictionary<int, bool> filterCategories;
        private Dictionary<int, bool> filterRetailers;

        private void Awake()
        {
            Instance = this;

            fetchDataUI.SetActive(true);

            itemCategoriesServiceHandler = new ItemCategoriesServiceHandler();
            itemServiceHandler = new ItemServiceHandler();
            retailerServiceHandler = new RetailerServiceHandler();
            userServiceHandler = new UserServiceHandler();

            filterCategories = new Dictionary<int, bool>();
            filterRetailers = new Dictionary<int, bool>();
        }

        private IEnumerator Start()
        {
            yield return itemCategoriesServiceHandler.GetItemCategories((itemCategories) => this.itemCategories = itemCategories);
            yield return SetFilterCategories();
            yield return retailerServiceHandler.GetRetailers((retailers) => this.retailers = retailers);
            yield return SetFilterRetailers();

            fetchDataUI.SetActive(false);
            mainUI.TriggerItems();
        }

        private IEnumerator SetFilterCategories()
        {
            foreach (ItemCategory itemCategory in itemCategories)
            {
                if (!PlayerPrefs.HasKey(CATEGORY_ID + itemCategory.id))
                {
                    PlayerPrefs.SetString(CATEGORY_ID + itemCategory.id, "true");
                }

                string stringValue = PlayerPrefs.GetString(CATEGORY_ID + itemCategory.id);
                bool.TryParse(stringValue, out bool value);

                filterCategories.Add(itemCategory.id, value);

                yield return null;
            }
        }

        private IEnumerator SetFilterRetailers()
        {
            foreach (Retailer retailer in retailers)
            {
                if (!PlayerPrefs.HasKey(RETAILER_ID + retailer.id))
                {
                    PlayerPrefs.SetString(RETAILER_ID + retailer.id, "true");
                }

                string stringValue = PlayerPrefs.GetString(RETAILER_ID + retailer.id);
                bool.TryParse(stringValue, out bool value);

                filterRetailers.Add(retailer.id, value);

                yield return null;
            }
        }

        public void UpdateFilterCategory(int id, bool value)
        {
            PlayerPrefs.SetString(CATEGORY_ID + id, value.ToString());
            filterCategories[id] = value;
        }

        public void UpdateFilterRetailer(int id, bool value)
        {
            PlayerPrefs.SetString(RETAILER_ID + id, value.ToString());
            filterRetailers[id] = value;
        }

        public bool GetFilterCategoryValue(int id)
        {
            return filterCategories[id];
        }

        public bool GetFilterRetailerValue(int id)
        {
            return filterRetailers[id];
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

        public IEnumerator LoadItems(Action<Item[]> callback)
        {
            yield return itemServiceHandler.GetItems((items) => this.items = items);
            callback?.Invoke(items);
        }

        public IEnumerator Register(Action<AuthenticationResponse> callback, User user)
        {
            yield return StartCoroutine(userServiceHandler.LoginOrRegister((authenticationResponse) => callback?.Invoke(authenticationResponse), user, true));

        }
        public IEnumerator Login(Action<AuthenticationResponse> callback, User user)
        {
            yield return StartCoroutine(userServiceHandler.LoginOrRegister((authenticationResponse) => callback?.Invoke(authenticationResponse), user, false));        
        }
    }
}
