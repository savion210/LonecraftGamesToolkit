using EventSystem;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("LonecraftGames/UI/Panels/UIPanel")]
    public class UIPanel : MonoBehaviour
    {
        [Header("Panel Type")] public Enums.UIPanelType panelType;

        [Header("Event Channels")] [SerializeField]
        private PanelTypeEvent onRegisterPanel;

        [Header("Canvas Group")] [SerializeField]
        private CanvasGroup canvasGroup;

        [Header("Fade Settings")] [SerializeField]
        private bool fadeOnShow = true;

        [Header("Fade Duration")] [SerializeField]
        private float fadeDuration = 1f;

        private Coroutine _fadeCoroutine;

        private void Start()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            onRegisterPanel.Raise(this); // Register the panel
        }


        #region Functions

        public void Show()
        {
            gameObject.SetActive(true);

            if (canvasGroup != null)
            {
                // Set initial alpha to 0 to make it fully transparent
                canvasGroup.alpha = 0;
                if (fadeOnShow)
                {
                    FadePanel(0f, 1f, fadeDuration);
                }
                else
                {
                    canvasGroup.alpha = 1;
                }
            }
        }

        public void Hide()
        {
            if (canvasGroup != null)
            {
                gameObject.SetActive(false);
            }
        }

        private void FadePanel(float startAlpha, float endAlpha, float duration)
        {
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }

            _fadeCoroutine =
                StartCoroutine(PanelFadeRoutine.FadeCanvasGroup(canvasGroup, startAlpha, endAlpha, duration));
        }

        #endregion
    }
}