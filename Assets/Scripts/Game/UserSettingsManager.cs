using ExordiumGamesAssignment.Scripts.Api.Models;
using ExordiumGamesAssignment.Scripts.Game;
using System.Collections.Generic;
using UnityEngine;

public class UserSettingsManager : MonoBehaviour
{
    public static UserSettingsManager Instance { get; private set; }

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
        string selectedRetailersJson = JsonUtility.ToJson(filterRetailers);

        PlayerPrefs.SetString(SelectedRetailersKey + username, selectedRetailersJson);
        PlayerPrefs.Save();
    }

    public void SaveSelectedUserCategoriesSettings()
    {
        string selectedCategoriesJson = JsonUtility.ToJson(filterCategories);

        PlayerPrefs.SetString(SelectedCategoriesKey + username, selectedCategoriesJson);
        PlayerPrefs.Save();
    }

    public void SaveSelectedUserFavoritesSettings()
    {
        string selectedFavoritesJson = JsonUtility.ToJson(filterFavorites);

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

    public void UpdateFavorite(int id)
    {
        filterFavorites.Add(id);
    }

    public void LoadDefaultSettings()
    {
        username = "default";

        ItemCategory[] itemCategories = GameManager.Instance.GetItemCategories();
        Retailer[] retailers = GameManager.Instance.GetRetailers();

        foreach (ItemCategory itemCategory in itemCategories)
        {
            filterCategories.Add(itemCategory.id, true);
        }

        foreach (Retailer retailer in retailers)
        {
            filterRetailers.Add(retailer.id, true);
        }

        filterFavorites.Clear();
    }

    public void LoadUserSettings(string username)
    {
        this.username = username;

        string selectedCategoriesJson = PlayerPrefs.GetString(SelectedCategoriesKey + username);
        filterCategories = JsonUtility.FromJson<Dictionary<int, bool>>(selectedCategoriesJson);

        string selectedRetailersJson = PlayerPrefs.GetString(SelectedRetailersKey + username);
        filterRetailers = JsonUtility.FromJson<Dictionary<int, bool>>(selectedRetailersJson);

        string selectedFavoritesJson = PlayerPrefs.GetString(SelectedFavoritesKey + username);
        filterFavorites = JsonUtility.FromJson<List<int>>(selectedFavoritesJson);
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
}
