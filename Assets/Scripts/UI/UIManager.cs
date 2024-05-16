using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private OverlayTransitionUI overlayTransitionUI;
        [SerializeField] private List<GameObject> allUIElements = new List<GameObject>();

        public IEnumerator ActivateUIElement(MonoBehaviour activeElement, Action callback = default)
        {
            yield return overlayTransitionUI.HideOverlay();
            foreach (GameObject element in allUIElements)
            {
                if (element == activeElement.gameObject)
                {
                    element.gameObject.SetActive(true);
                }
                else
                {
                    element.gameObject.SetActive(false);
                }
            }
            yield return overlayTransitionUI.ShowOverlay();
            callback?.Invoke();
        }
    }
}
