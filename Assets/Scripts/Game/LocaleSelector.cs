using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public class LocaleSelector : MonoBehaviour
    {
        private bool active = false;
        public void ChangeLocale(int localeId)
        {
            if (active == true) return;

            StartCoroutine(SetLocale(localeId));
        }

        private IEnumerator SetLocale(int _localeId)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeId];
            active = false;
        }
    }
}
