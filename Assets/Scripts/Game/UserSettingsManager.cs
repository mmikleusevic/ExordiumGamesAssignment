using ExordiumGamesAssignment.Scripts.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public class UserSettingsManager : MonoBehaviour
    {
        public static UserSettingsManager Instance { get; private set; }

        public event Action OnFiltersChanged;

        private const string SelectedCategoriesKey = "SelectedCategories";
        private const string SelectedRetailersKey = "SelectedRetailers";
        private const string SelectedFavoritesKey = "SelectedFavorites";

        private Dictionary<int, bool> filterCategories;
        private Dictionary<int, bool> filterRetailers;
        private List<int> filterFavorites;

        private string username;

        private void Awake()
        {
            Instance = this;

            filterCategories = new Dictionary<int, bool>();
            filterRetailers = new Dictionary<int, bool>();
            filterFavorites = new List<int>();
        }

        public void SaveSelectedUserRetailersSettings()
        {
            string selectedRetailersJson = JsonConvert.SerializeObject(filterRetailers);

            PlayerPrefs.SetString(SelectedRetailersKey + username, selectedRetailersJson);
            PlayerPrefs.Save();
        }

        public void SaveSelectedUserCategoriesSettings()
        {
            string selectedCategoriesJson = JsonConvert.SerializeObject(filterCategories);

            PlayerPrefs.SetString(SelectedCategoriesKey + username, selectedCategoriesJson);
            PlayerPrefs.Save();
        }

        public void SaveSelectedUserFavoritesSettings()
        {
            string selectedFavoritesJson = JsonConvert.SerializeObject(filterFavorites);

            PlayerPrefs.SetString(SelectedFavoritesKey + username, selectedFavoritesJson);
            PlayerPrefs.Save();
        }

        public void UpdateFilterCategory(int id, bool value)
        {
            filterCategories[id] = value;
        }

        public void UpdateFilterRetailer(int id, bool value)
        {
            filterRetailers[id] = value;
        }

        public void AddFavorite(int id)
        {
            filterFavorites.Add(id);
        }

        public void RemoveFavorite(int id)
        {
            filterFavorites.Remove(id);
        }

        public void LoadDefaultSettings()
        {
            username = "default";

            LoadDefaultCategories();
            LoadDefaultRetailers();

            filterFavorites.Clear();

            OnFiltersChanged?.Invoke();
        }

        private void LoadDefaultCategories()
        {
            ItemCategory[] itemCategories = GameManager.Instance.GetItemCategories();
            filterCategories.Clear();

            foreach (ItemCategory itemCategory in itemCategories)
            {
                filterCategories.Add(itemCategory.id, true);
            }
        }

        private void LoadDefaultRetailers()
        {
            Retailer[] retailers = GameManager.Instance.GetRetailers();
            filterRetailers.Clear();

            foreach (Retailer retailer in retailers)
            {
                filterRetailers.Add(retailer.id, true);
            }
        }

        public void LoadUserSettings(string username)
        {
            this.username = username;

            string selectedCategoriesJson = PlayerPrefs.GetString(SelectedCategoriesKey + username);
            if (IsJsonValid(selectedCategoriesJson, '{', '}'))
            {
                filterCategories = (Dictionary<int, bool>)JsonConvert.DeserializeObject(selectedCategoriesJson, typeof(Dictionary<int, bool>));
            }
            else
            {
                LoadDefaultCategories();
            }

            string selectedRetailersJson = PlayerPrefs.GetString(SelectedRetailersKey + username);
            if (IsJsonValid(selectedRetailersJson, '{', '}'))
            {

                filterRetailers = (Dictionary<int, bool>)JsonConvert.DeserializeObject(selectedRetailersJson, typeof(Dictionary<int, bool>));
            }
            else
            {
                LoadDefaultRetailers();
            }

            string selectedFavoritesJson = PlayerPrefs.GetString(SelectedFavoritesKey + username);
            if (IsJsonValid(selectedFavoritesJson, '[', ']'))
            {
                filterFavorites = (List<int>)JsonConvert.DeserializeObject(selectedFavoritesJson, typeof(List<int>));
            }
            else
            {
                filterFavorites.Clear();
            }

            OnFiltersChanged?.Invoke();
        }

        public bool GetFilterCategoryValue(int id)
        {
            return filterCategories[id];
        }

        public bool GetFilterRetailerValue(int id)
        {
            return filterRetailers[id];
        }

        public bool GetFilterFavoriteValue(int id)
        {
            return filterFavorites.Contains(id);
        }

        public List<int> GetFavorites()
        {
            return filterFavorites;
        }

        public bool IsJsonValid(string input, char char1, char char2)
        {
            if (input.Length > 2) return true;
            if (input.Length == 0) return false;

            int count1 = 0;
            int count2 = 0;

            foreach (char c in input)
            {
                if (c == char1)
                {
                    count1++;
                }
                else if (c == char2)
                {
                    count2++;
                }
                else
                {
                    return true;
                }

                if (count1 > 1 || count2 > 1) return true;
            }

            return (count1 + count2) != 2;
        }
    }
}
