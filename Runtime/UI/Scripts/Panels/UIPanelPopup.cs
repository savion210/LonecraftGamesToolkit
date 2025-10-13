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

        [SerializeField] private float fadeDuration = 1f;

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
#if DOTWEEN
                canvasGroup.DOFade(1, fadeDuration).SetEase(Ease.Linear);
#else
                canvasGroup.alpha = 1;
#endif
            }
        }

        public void Hide()
        {
            if (canvasGroup != null)
            {
                // Set initial alpha to 1 to make it fully opaque
                //canvasGroup.alpha = 1;

                //         canvasGroup.DOFade(0, fadeDuration).SetEase(Ease.Linear)
                //           .OnComplete(() =>
                //         {
                gameObject.SetActive(false);
                //       });
            }
        }

        #endregion
    }
}