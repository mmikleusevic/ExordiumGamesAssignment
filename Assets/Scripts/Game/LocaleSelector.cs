using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public class LocaleSelector : MonoBehaviour
    {
        public readonly string STRING_TABLE = "ExordiumGamesStringTable";
        public readonly string ASSET_TABLE = "ExordiumGamesAssetTable";
        private readonly string LOCALE_ID = "localeId";

        public static LocaleSelector Instance { get; private set; }

        private bool active = false;

        private int currentLocaleId = -1;
        private int changedLocaleId = 0;


        private void Awake()
        {
            Instance = this;

            if (PlayerPrefs.HasKey(LOCALE_ID))
            {
                currentLocaleId = PlayerPrefs.GetInt(LOCALE_ID);
                ChangeLocale(currentLocaleId);
            }
            else
            {
                SaveDefaultLocale();
                currentLocaleId = 0;
            }
        }

        public void ChangeLocale(int localeId)
        {
            if (active == true) return;

            StartCoroutine(SetLocale(localeId));
        }

        private IEnumerator SetLocale(int localeId)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
            changedLocaleId = localeId;
            active = false;
        }

        public void SaveDefaultLocale()
        {
            PlayerPrefs.SetInt(LOCALE_ID, changedLocaleId);
            PlayerPrefs.Save();
            currentLocaleId = changedLocaleId;
        }

        public void Cancel()
        {
            StartCoroutine(SetLocale(currentLocaleId));
        }
    }
}
