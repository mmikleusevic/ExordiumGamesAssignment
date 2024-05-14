using System.Collections.Generic;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> allUIElements = new List<GameObject>();

        public void ActivateUIElement(MonoBehaviour activeElement)
        {
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
        }
    }
}
