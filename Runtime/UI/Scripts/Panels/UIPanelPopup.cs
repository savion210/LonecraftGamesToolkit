#if DOTWEEN
using DG.Tweening;
#endif
using EventSystem;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.UI
{
    [AddComponentMenu("LonecraftGames/UI/Panels/UIPanelPopup")]
    public class UIPanelPopup : MonoBehaviour
    {
        [Header("Popup Type")] public Enums.UIPopupType popupType;

        [Header("Event Channels")] [SerializeField]
        private PopupTypeEvent onRegisterPopup;

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

            onRegisterPopup.Raise(this); // Register the panel
        }

        #region Functions

        public void Show()
        {
            gameObject.SetActive(true);
            if (canvasGroup != null)
            {
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