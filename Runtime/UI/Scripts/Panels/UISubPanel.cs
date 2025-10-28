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

        [Header("Fade Settings")] [SerializeField]
        private bool fadeOnShow = true;

        [Header("Fade Duration")] [SerializeField]
        private float fadeDuration = 1f;

        private Coroutine _fadeCoroutine;

        private void Start()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            onRegisterSubPanel.Raise(this); // Register the subpanel
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

        /// <summary>
        ///  Hides the subpanel with a fade-out effect.
        /// </summary>
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