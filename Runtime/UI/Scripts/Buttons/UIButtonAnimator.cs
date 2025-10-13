using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LonecraftGames.Toolkit.UI
{
    public class UIButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Animation Settings")]
        [SerializeField] private float hoverScale = 1.1f;
        [SerializeField] private float pressedScale = 0.95f;
        [SerializeField] private float duration = 0.15f;
        [SerializeField] private Ease easeType = Ease.OutBack;

        private Vector3 originalScale;
        private Tween currentTween;

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            AnimateTo(originalScale * hoverScale);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            AnimateTo(originalScale);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AnimateTo(originalScale * pressedScale);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            AnimateTo(originalScale * hoverScale);
        }

        private void AnimateTo(Vector3 target)
        {
            currentTween?.Kill();
            currentTween = transform.DOScale(target, duration).SetEase(easeType);
        }
    }
}