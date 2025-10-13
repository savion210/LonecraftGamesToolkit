#if DOTWEEN
using DG.Tweening;
#endif
using EventSystem;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("LonecraftGames/UI/Panels/UISubPanel")]
    public class UISubPanel : MonoBehaviour
    {
        [Tooltip("The type of subpanel this is.")] [Header("SubPanel Type")]
        public Enums.SubPanelType subPanelType;

        [Tooltip("Event channel to register this subpanel with the UI manager.")]
        [Header("Event Channels")]
        [SerializeField]
        private SubPanelTypeEvent onRegisterSubPanel;

        [SerializeField] private SubPanelEnumEvent onSubPanelChange;

        [Header("Canvas Group")] [SerializeField]
        private CanvasGroup canvasGroup;

        [Tooltip("Duration of the fade-in and fade-out effects.")] [Header("Fade Settings")] [SerializeField]
        private float fadeDuration = 1f;

        private void Start()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }


        #region Functions

        /// <summary>
        ///  Registers the subpanel with the UI manager.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);

            if (canvasGroup != null)
            {
                // Set initial alpha to 0 to make it fully transparent
                canvasGroup.alpha = 0;

#if DOTWEEN
                canvasGroup.DOFade(1, fadeDuration).SetEase(Ease.Linear);
#else
                canvasGroup.alpha = 1;
#endif
            }
        }

        /// <summary>
        ///  Hides the subpanel with a fade-out effect.
        /// </summary>
        public void Hide()
        {
            if (canvasGroup != null)
            {
                // Set initial alpha to 1 to make it fully opaque
                //      canvasGroup.alpha = 1;

                //     canvasGroup.DOFade(0, fadeDuration).SetEase(Ease.Linear)
                //       .OnComplete(() =>
                //       {
                gameObject.SetActive(false);
                //      });
            }
        }

        #endregion
    }
}