using UnityEngine;

namespace FreedLOW.Painting.Extensions
{
    public static class UIExtension
    {
        public static void State(this CanvasGroup canvasGroup, bool state)
        {
            canvasGroup.alpha = state ? 1.0f : 0.0f;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }
    }
}