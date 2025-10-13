using DG.Tweening;
using EventSystem;
using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("LonecraftGames/UI/Panels/UIPanel")]
    public class UIPanel : MonoBehaviour
    {
        [Header("Panel Type")]
        public Enums.UIPanelType panelType;

        [Header("Event Channels")] 
        [SerializeField] private PanelTypeEvent onRegisterPanel;

        [Header("Canvas Group")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 1f;

        private void Start()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            onRegisterPanel.Raise(this); // Register the panel
        }
        

        #region  Functions

        public void Show()
        {
            gameObject.SetActive(true);

            if (canvasGroup != null)
            {
                // Set initial alpha to 0 to make it fully transparent
                canvasGroup.alpha = 0;
                
                canvasGroup.DOFade(1, fadeDuration).SetEase(Ease.Linear);
            }
        }

        public void Hide()
        {
            if (canvasGroup != null)
            {
                // Set initial alpha to 1 to make it fully opaque
            //   canvasGroup.alpha = 1;
                
           //     canvasGroup.DOFade(0, fadeDuration).SetEase(Ease.Linear)
             //       .OnComplete(() =>
               //     {
                        gameObject.SetActive(false);
               //     });
            }
        }

        #endregion
    }
}