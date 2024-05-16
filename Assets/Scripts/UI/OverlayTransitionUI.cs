using System.Collections;
using UnityEngine;

namespace ExordiumGamesAssignment.Scripts.UI
{
    public class OverlayTransitionUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public float transitionDuration = 0.25f;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
        }

        public IEnumerator ShowOverlay()
        {
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, transitionDuration));
        }

        public IEnumerator HideOverlay()
        {
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f, transitionDuration));
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
        {
            float startTime = Time.time;
            while (Time.time < startTime + duration)
            {
                cg.alpha = Mathf.Lerp(start, end, (Time.time - startTime) / duration);
                yield return null;
            }
            cg.alpha = end;
        }
    }
}
